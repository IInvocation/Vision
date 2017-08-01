# README #

## Vision ##

### What is this repository for? ###

* This repository is meant to allow remote-controlling your computer in a secure manner
* It's written to establish a custom "SmartHome" software that can be integrated with other protocols
* Theres a "main" server, that is reponsible for Authentication and Authorization - as well as bundling other protocols to
  a custom protocol to keep the Endpoints simple
* The endpoints only speaks the language of the custom protocol, are secured using https and get the authorization from the main server
  (interacting in ANY way with the endpoint therefore requires authentication by the main server)
* The server integrates with Alexa and knows 2 "user"-API's called Jarvis and Friday, where Jarvis represents the computer of the user,
  and Friday represents the "SmartHome"-Device of the customer (always on device, like a Raspberry Pi)
* Configuration on what is allowed for which API is done and stored with the endpoints to leave users full control

### How do I get set up? ###

* This repository depends on https://github.com/IInvocation/AppFx, please clone that repository first and follow the given instructions
* To use AppFx, please add your nuget-share "\\localhost\nuget\" to your solutions nuget-sources
* To even start - u'll need to setup the database powering your "main"-server (a sample .sql-File is in the project)
* To run any of the hosts - u'll need to add some appsettings, collected in a file called "appsettings.secret.json"
  This requrires an Amazon-Developer-Account, a Google-Developer-Account and some entries of the database mentioned before
  All needed secrets are commented as "secret" on the appsettings.json-File

### Help? ###
* Feel free to contact me. I'd be happy about some support and instructional help.
