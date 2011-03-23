Feature: Working with Lambda Specifications
	In order reuse ordinary rule im my domain
	As a developer
	I want create specification classes based in lambda
	So That I could use these classes to validade rules and performe queries

Scenario: Checking if a discontinued Product is discontinued
	Given I have a discontinued Product
	And I have Product Discontinued Specification
	When I check if the product is discontinued
	Then the result should be true

Scenario: Checking if a not discontinued Product is discontinued
	Given I have a not discontinued Product
	And I have Product Discontinued Specification
	When I check if the product is discontinued
	Then the result should be false
