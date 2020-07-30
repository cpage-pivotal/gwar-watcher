# GWAR-Watcher
The automation tool that we can use to monitor GWAR events and validate messages/payloads.  Currently it follows this process:
1. Grab as many events off of Kafka as have not been committed
2. Validate those events
3. Check if that event's action ID exists in Elastic
4. For events not found in Kafka, get the most recent 25 from elastic
5. Validate those events

A Blazor server, table-based UI is then displayed with the information about the events gathered and validated.