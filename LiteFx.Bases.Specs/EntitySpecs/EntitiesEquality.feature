Feature: Entities Equality
	In order to compare to entities
	As a developer
	I want that entities follows the rules of DDD

Scenario: Comparing two entities of the same type and id
	Given I have a product instance with the id 5
	And I have another product instance with the id 5
	When I compare the two instances
	Then the equality should be true

Scenario: Comparing two entities of the same type and diferent id
	Given I have a product instance with the id 5
	And I have another product instance with the id 10
	When I compare the two instances
	Then the equality should be false

Scenario: Comparing two entities of diferent type and same id
	Given I have a product instance with the id 5
	And I have a category instance with the id 5
	When I compare the two instances
	Then the equality should be false