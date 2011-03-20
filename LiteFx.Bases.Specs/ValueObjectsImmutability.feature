Feature: ValueObjects Immutability
	In order to create a ValueObject
	As a developer
	I want to be told when my ValueObject is not immutable

Scenario: Create a mutable ValueObject
	Given a mutable ValueObject
	When I instantiate this mutable object
	Then a exception need to be throw

Scenario: Create a Immutable ValueObject
	Given a immutable ValueObject
	When I instantiate this immutable object
	Then it can not be null