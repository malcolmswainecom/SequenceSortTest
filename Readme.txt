Get the project
Clone or download and open the SequenceService.sln file in visual studio 2019
https://github.com/malcolmswainecom/SequenceSortTest.git

Build the solution
Build the entire solution and restore any nuget packages

Run data migration to build a local db in the Sequence.Data project
From the Package Manager console: Update-database

Run the Sequence.Web.Api project
Run the Sequence.Web.Api project should load the default swagger UI 


Remarks
Data is saved horizontally - in real application would save vertically and in numeric form using hashing function for a lookup index since checking for existing sequences vertically has some overhead

TODO: Basic console logging is baked in, but would like to add global exception handler with some file logging  



