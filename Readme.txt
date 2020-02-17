Get the project
Clone or download and open the SequenceService.sln file in visual studio 2019
https://github.com/malcolmswainecom/SequenceSortTest.git

Build the solution
Build the entire solution and restore any nuget packages

Run data migration to build a local db in the Sequence.Data project
From the Package Manager console: Update-database

Run the Sequence.Web.Api project
Set Sequence.Web.Api project as startup and F5. Swagger UI should be loaded by default to expose the WebApi endpoints


Remarks
Data is saved horizontally - in real application would save vertically and in numeric form using hashing function for a lookup index since checking for existing sequences vertically has some overhead


