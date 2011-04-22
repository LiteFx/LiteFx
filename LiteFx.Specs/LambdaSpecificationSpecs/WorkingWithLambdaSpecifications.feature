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
	
Scenario: Checking if a discontinued Derived Product is discontinued
	Given I have a discontinued Derived Product
	And I have Product Discontinued Specification
	When I check if the product is discontinued
	Then the result should be true

Scenario: Checking if a not discontinued Product is discontinued
	Given I have a not discontinued Product
	And I have Product Discontinued Specification
	When I check if the product is discontinued
	Then the result should be false

Scenario: Combining LambdaSpecifications with and operator
	Given I have a discontinued Product
	And I have Product Discontinued Specification
	And a Price Specification
	When I check if the product satisfy the two specifications
	Then the result should be true

Scenario: Combining LambdaSpecifications with or operator
	Given I have a not discontinued Product
	And I have Product Discontinued Specification
	And a Price Specification
	When I check if the product satisfy one of the two specifications
	Then the result should be true