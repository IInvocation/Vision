# TODO #

Vision

### Server-Optimizations ###

* create static subdomain for static files
* try ARR-performance
* try docker(-swarm)

### Server-Fixes ###
* Try to disable URL-Rewrite for external domains

### AppFx.Data ##
* consisder removing direct-logs

### Authentication ###
* add external OpenIdConnect (Google, Amazon, ...)
* add internal OpenIdConnect

### Internal OpenIdConnect ###
* properly implement storage and validation for identityserver
* implement ResourceOwner-Grant (for clients like jarvis/friday)
* implement Authorization-Code-Grant (for clients like vision)

### UnitTesting ###
* lots... lots of work

### Logging ###
* Add db-log-target