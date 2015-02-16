---
title: Design
summary: The design of SQL Server transport
tags:
- SQL Server
- Design
---

The SQL Server transport implements a message queueing mechanism on top of a Microsoft SQL Server database. It uses plain tables to model queues. It does not make any use of ServiceBroker, a messaging technology built into the SQL Server, mainly due to cumbersome API and difficult maintenance. 

The SQL Server transport is a hybrid queueing system which is neither store-and-forward (like MSMQ) nor a broker (like RabbitMQ). It treats the SQL Server only as a storage infrastructure for the queues. The queueing logic itself is implemented and executed as part of the transport code running in an NServiceBus endpoint. 

In the simplest form, SQL Server transport it uses a single instance of the SQL Server to maintain all the queues for all endpoints of a system. In order to send a message, an endpoint needs to connect to the (usually remote) database server and execute a SQL command. The message is delivered directly to the destination queue without any store-and-forward mechanism. Such a simplistic approach can only be only used for very small projects because of the need to store everything in a single database. The upside is it does not require Distributed Transaction Coordinator (MS DTC).

In a more complex scenario endpoints do use separate databases and DTC is involved. Due to lack of store-and-forward mechanism, if a remote endpoint's database is down, our endpoint cannot send messages to it, potentially rendering our endpoint (and endpoints depending on it) unavailable. 

SQL Server transport is not limited to the described simple form. The Outbox feature can be used to effectively implement a distributed decoupled architecture where:
 * Each endpoint has its own database where it stores both the queues and the user data
 * Messages are not sent immediately upon calling `Bus.Send()` but rather are added to the *outbox* that is stored in the endpoints own database. After completing the handling logic the messages in the *outbox* are forwarded to their destination databases
 * Should one of the forward operations fail, it will be retried by means of standard NServiceBus retry mechanism (both first-level and second-level)