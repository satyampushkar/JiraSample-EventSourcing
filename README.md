# JiraSample-EventSourcing
Learn &amp; understand EventSourcing by creating a simple Jira like demo application

This project 
* Provides 2 set of APIs: Read/command & Write, in turn 2 different set of read & write db
* `JiraSample.Api` uses
 * Events to capture each actions performed on domain entity(Jira Item in this case)
 * Stores the events on MongoDb based EventStore.
 * Publishes events to Kafka
* `JiraSampleQuery..Api`
 * Uses projections to provide different set of read options.
 * Subscribes to events of Kafka
 * Stores read projections on MS Sql 
* It also uses Clean Architecture on both read & write side. 

--------------------------------------------------
ToDo List:
* How to run Guide
* Add a frontend ui for demo purpose 
* Add
  * Authentication
  * Mapping(Mapster)
  * Db Migration & seeding logic
  * Unit tests
* Improve
  * Exception Handling
  * Validation logic
* Fix
  * Parent - Child(ren) Mapping
  * option-config in respective dependency classes
