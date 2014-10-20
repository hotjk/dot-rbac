# Grit.Demo.CQRS #

### Setup RabbitMQ ###

* Install RabblitMQ
* Start RabbitMQ service
* Add vhost/user/permission, run below commands

rabbitmqctl add_vhost grit_demo_vhost

rabbitmqctl add_user event_user event_password

rabbitmqctl set_permissions -p grit_demo_vhost event_user ".*" ".*" ".*"

### Setup MySQL ###

* Install MySQL
* Inport Dump20140707.sql

### Run ###

* CQRS.Demo.Sagas
* CQRS.Demo.Web