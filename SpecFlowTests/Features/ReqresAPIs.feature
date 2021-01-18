Feature: ReqresAPIs
	Test the main APIs on Reqres to make sure they works

@Users
Scenario: Get list of Users
	Given the user page number is 2
	When send the users request
	Then the count of returned users should be > 0

@CreateUser
Scenario: Create a user
	Given the user name is Tom
	And the user's job is QA
	When send the create user request
	Then the user should get an id

@Resources
Scenario: Get list of Resources
	When send the resources request
	Then the count of returned resources should be > 0