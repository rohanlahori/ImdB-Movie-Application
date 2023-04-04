Feature: Imdb_Application
@addMovie
Scenario: Add_Movie
	Given user provides the inputs for the movie
	| MovieName     | YearOfRelease | Plot   | Actors | Producer |
	| Shershah      | 2019          | Army   | 1,2    |    1     |
	When Add Movie to the database
	Then Movie List would look like '[{"Id":1,"Name":"Pathan","YearOfRelease":2020,"Plot":"Fun","Actors":[{"Name":"Ranbir Kapoor","Id":1,"DateOfBirth":"1990-10-20T00:00:00"},{"Name":"Ranbir Singh","Id":2,"DateOfBirth":"1994-11-21T00:00:00"}],"Producer":{"Name":"Ajay Kapoor","Id":1,"DateOfBirth":"1980-10-21T00:00:00"}},{"Id":2,"Name":"Shershah","YearOfRelease":2021,"Plot":"Army","Actors":[{"Name":"Ranbir Kapoor","Id":1,"DateOfBirth":"1990-10-20T00:00:00"},{"Name":"Ranbir Singh","Id":2,"DateOfBirth":"1994-11-21T00:00:00"}],"Producer":{"Name":"Ajay Kapoor","Id":1,"DateOfBirth":"1980-10-21T00:00:00"}},{"Id":3,"Name":"Shershah","YearOfRelease":2019,"Plot":"Army","Actors":[{"Name":"Ranbir Kapoor","Id":1,"DateOfBirth":"1990-10-20T00:00:00"},{"Name":"Ranbir Singh","Id":2,"DateOfBirth":"1994-11-21T00:00:00"}],"Producer":{"Name":"Ajay Kapoor","Id":1,"DateOfBirth":"1980-10-21T00:00:00"}}]'
@addMovie
Scenario Outline: AddMovie Invalid inputs
	Given user provides the inputs for the movie '<MovieName>','<YearOfRelease>','<Plot>','<Actors>','<Producer>'
	When Add Movie to the database
	Then error with the message '<errorMessage>'would be shown
	Examples: 
	| MovieName | YearOfRelease | Plot | Actors | Producer | errorMessage                        |
	|           | 2018          | Fun  | 1      | 1        | Invalid name for the movie          |
	| Pathan    |               | Fun  | 1      | 1        | Invalid year of release             |
	| YJHD      | 2019          |      | 1,2    | 1        | Invalid plot for the movie          |
	| ABCD      | 2013          | Fun  |        | 1        | Add , seprated values for the actor |
	| ABCD2     | 2017          | Fun  | 2      |          | Invalid producer for the movie      |
@listMovie
Scenario:List_Movie
	When List all the movies stored in the database
	Then Movie List would look like '[{"Id":1,"Name":"Pathan","YearOfRelease":2020,"Plot":"Fun","Actors":[{"Name":"Ranbir Kapoor","Id":1,"DateOfBirth":"1990-10-20T00:00:00"},{"Name":"Ranbir Singh","Id":2,"DateOfBirth":"1994-11-21T00:00:00"}],"Producer":{"Name":"Ajay Kapoor","Id":1,"DateOfBirth":"1980-10-21T00:00:00"}},{"Id":2,"Name":"Shershah","YearOfRelease":2021,"Plot":"Army","Actors":[{"Name":"Ranbir Kapoor","Id":1,"DateOfBirth":"1990-10-20T00:00:00"},{"Name":"Ranbir Singh","Id":2,"DateOfBirth":"1994-11-21T00:00:00"}],"Producer":{"Name":"Ajay Kapoor","Id":1,"DateOfBirth":"1980-10-21T00:00:00"}}]'
@deleteMovie
Scenario: Delete_Movie
	Given the user provides id of the movie to be deleted as 1
	When Delete the movie from the database
	Then Movie List would look like '[{"Id":2,"Name":"Shershah","YearOfRelease":2021,"Plot":"Army","Actors":[{"Name":"Ranbir Kapoor","Id":1,"DateOfBirth":"1990-10-20T00:00:00"},{"Name":"Ranbir Singh","Id":2,"DateOfBirth":"1994-11-21T00:00:00"}],"Producer":{"Name":"Ajay Kapoor","Id":1,"DateOfBirth":"1980-10-21T00:00:00"}}]'
@deleteMovie
Scenario: DeleteMovie Invalid ID
	Given the user provides id of the movie to be deleted as 100
	When Delete the movie from the database
	Then error with message "Invalid Movie ID" would be shown