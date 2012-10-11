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

Scenario: Comparing the Hash Code of two entities of the same type and id
	Given I have a product instance with the id 5
	And I have another product instance with the id 5
	When I compare the Hash Code of this two instances
	Then the equality should be true

Scenario: Comparing the Hash Code of two entities of the same type and diferent id
	Given I have a product instance with the id 5
	And I have another product instance with the id 10
	When I compare the Hash Code of this two instances
	Then the equality should be false

Scenario: Comparing the Hash Code of two two entities of diferent type and same id
	Given I have a product instance with the id 5
	And I have a category instance with the id 5
	When I compare the Hash Code of this two instances
	Then the equality should be false

Scenario: Comparing two null entities
	Given I have a null product
	And I have another null product
	When I compare the two instances
	Then the equality should be true

Scenario: Comparing two entities one null and another not
	Given I have a null product
	And I have another product instance with the id 5
	When I compare the two instances
	Then the equality should be false

Scenario: Comparing two entities one not null and another null
	Given I have a product instance with the id 5
	And I have another null product
	When I compare the two instances
	Then the equality should be false

Scenario: Comparing the Hash Code of two entities of the same type and id equals 0
	Given I have a product instance with the id 0
	And I have another product instance with the id 0
	When I compare the Hash Code of this two instances
	Then the equality should be false

Scenario: Adding two entities of the same type and id into a HashSet
	Given I have a product instance with the id 5
	And I have another product instance with the id 5
	And a HashSet instance
	When I add the two instances to a HashSet
	Then the HashSet count should be 1

Scenario: Adding two entities of the same type and diferent id into a HashSet
	Given I have a product instance with the id 5
	And I have another product instance with the id 10
	And a HashSet instance
	When I add the two instances to a HashSet
	Then the HashSet count should be 2

Scenario: Adding two entities of diferent types and same id into a HashSet
	Given I have a product instance with the id 5
	And I have a category instance with the id 5
	And a HashSet instance
	When I add the two instances to a HashSet
	Then the HashSet count should be 2