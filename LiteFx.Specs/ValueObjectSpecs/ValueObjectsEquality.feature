Feature: Value Objects Equality
	In order to compare to Value Objects
	As a developer
	I want that value objects follows the rules of DDD

Scenario: Comparing two equal value objects instances
	Given I have a value object instance
	And I have another value object instance
	When I compare the two value objects instances
	Then the equality of the value objects should be true
