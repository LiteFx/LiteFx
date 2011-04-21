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

Scenario: Check a valid Derived Type
	Given a valid Derived Type
	When I call the validate method
	Then the count of validationResult collection should be 0

Scenario: Check a invalid Derived Type
	Given a invalid Derived Type
	When I call the validate method
	Then the count of validationResult collection should be 2