Configuration

Created with vs 2022
I chose to import most packages from CDN.

hardcoded api url/port: http://localhost:5230

packages:
Vue 3.4.18 (last time I worked with Vue was v1)
Axios 1.6.7
.Net 8
C# 8
Web.Api 2
Microsoft.AspNetCore.Mvc.NewtonSoftJson

I’ve met all the requirements except for a unit test project, but as part of the kiss/yagni principals the SWAGGER window IS the unit test platform

Things I would prefer to see before going into production

- Required fields at the UI level, implied due to math but user is not informed  (KISS)
- Values coming from a maintained database (KISS = I hardcoded the Vehicle Cost Algorithm options)
- Better Responsiveness at the UI level
- Better Styling
- Https (I don't have IIS installed on my laptop, so chose Http)
- CORS: not whitelist everything (see comment in code ProgiBidCalcTool.Server program.cs)
