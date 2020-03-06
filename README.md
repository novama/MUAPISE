# MUAPISE
Sample application to integrate multiple API services.
Author: Norman Valerio

## Folder structure (based on David Fowler Template)
(https://gist.github.com/davidfowl/ed7564297c61fe9ab814)

```
$/
  artifacts/
  build/
  docs/
  lib/
  packages/
  samples/
  src/
  tests/
  .gitignore
  NuGet.Co(fig
  LICENSE
  README.md
  {solution}.sln
```


- **`artifacts`** - Build outputs go here. Doing a build.cmd/build.sh generates artifacts here (nupkgs, dlls, pdbs, etc.)
- **`build`** - Build customizations (custom msbuild files/psake/fake/albacore/etc) scripts
- **`docs`** - Documentation stuff, markdown files, help files etc.
- **`lib`** - Things that can **NEVER** exist in a nuget package (binaries which are linked to in the source but are not distributed through NuGet).
- **`packages`** - NuGet packages
- **`samples`** (optional) - Sample projects
- **`src`** - Main projects (the product code).
- **`tests`** - Test projects.


## Excersise Goal:
Create an application that integrates multiple services into a useful and timesaving workflow.

### Objectives:
Build an application in a language listed below that provides an API that takes an IP or domain as input and gathers information from multiple sources returning a single result. Your application must farm individual portions of the lookup to various workers who perform the action. The application should then combine the results and return a single payload.

### The API should accept:
    - IP or domain
    - A list of services to query. This input should be optional with a default list used if none provided.

### The API should then:
    Validate input
    Break the request into multiple individual tasks
    Send those tasks to a pool of 2+ workers. The API must support workers on separate processes/machines/docker containers than the API.
    The workers should then perform the tasks and return the results to the application
    The application should wait for all tasks for a request to be completed, combine the results and return
    Some suggested services available to query:
        - GeoIP
        - RDAP
        - Reverse DNS
        - Ping

### Bonus Points:
    Add additional services (VirusTotal, open ports, website status, domain availability, etc)
    API has a Swagger Spec (if you use REST)
    Support partial results on error or rate limit


## Main API Specs (Project Muapise)
The MUAPISE main API endpoint by default (Console-Kestrel) is: https://localhost:8481/api/v1/hostdata/{Host_IP_or_Name}/{List_of_Services}
  - {Host_IP_or_Name} => Can be either an IPv4 address fromat or a full host domain name, ie. www.google.com
  - {List_of_Services} => (Optional) A string of multiple services names separated using "|".

    Available Services: { ping, reversedns, geoip, portstatus }
    Usage example: ping|portstatus
    * If no services are defined in the request, then a default list will be used. The default list of services in that case is: ping|geoip.

 Request example: [GET] https://localhost:8481/api/v1/hostdata/www.google.com/geoip|ping|reversedns

Muapise project Swagger UI tool route: http://localhost:8490/api-docs-tool/index.html
Muapise project Swagger API docs route: http://localhost:8490/api-docs/v1/swagger.json
    
    
## Worker API Specs (Project Muapise.QueryServiceWorker)
The worker API endpoint by default (Console-Kestrel) is: http://localhost:8490/workerapi/v1/{Resource}/{Host_IP_or_Name}
   - {Resource} => There are 5 different resources for the route: ping, portstatus, geoiplocation, rdap, reversedns.

Request example: [GET] http://localhost:8490/workerapi/v1/ping/www.google.com

Worker project Swagger UI tool route: http://localhost:8490/api-docs-tool/index.html
Worker project Swagger API docs route: http://localhost:8490/api-docs/v1/swagger.json


## Incomplete
Pooling handling for workers.
Full implementation of the RDAP service.
Adding Unit testing project.
    
    
    



