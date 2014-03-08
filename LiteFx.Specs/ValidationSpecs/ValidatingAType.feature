Feature: Validating a Type
	In order to make the developers life easier
	And improving the ubiquituous language in code
	As a developer
	I want to be told when a Type is valid or invalid

Scenario: Check a valid Type
	Given a valid Type
	When I call the validate method
	Then the count of validationResult collection should be 0

Scenario: Check a invalid Type
	Given a invalid Type
	When I call the validate method
	Then the count of validationResult collection should be 1

Scenario: Check a invalid Type skiping the validation
	Given a invalid Type
	And I call skip validation method
	When I call the validate method
	Then the count of validationResult collection should be 0

Scenario: Check a valid Derived Type
	Given a valid Derived Type
	When I call the validate method
	Then the count of validationResult collection should be 0

Scenario: Check a invalid Derived Type
	Given a invalid Derived Type
	When I call the validate method
	Then the count of validationResult collection should be 2

Scenario: Check a valid Type with a Nullable member
	Given a valid Type with a Nullable member
	When I call the validate method
	Then the count of validationResult collection should be 0

Scenario: Check a invalid Type with a Nullable member
	Given a invalid Type with a Nullable member
	When I call the validate method
	Then the count of validationResult collection should be 2

Scenario: Check a valid Type with a Nullable member set to null
	Given a valid Type with a Nullable member set to null
	When I call the validate method
	Then the count of validationResult collection should be 0

Scenario: Get client validation from a Type
	Given a valid Type
	When I call the GetClientValidationData method passing the property Name
	Then the client validation rule collection should be have the required rule