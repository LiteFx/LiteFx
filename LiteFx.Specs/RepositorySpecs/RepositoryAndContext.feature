Feature: Working on a Context trough a Repository
	In order to use the repository pattern
	and make my software rocks with this pattern
	As a developer
	I want to manipulate a ORM context trough a Repository

Scenario: Selecting an entity by it's id
	Given a Context
	And a Repository
	When I call the GetById method using the valid id 1
	Then a entity instance with the id 1 should be returned

Scenario: Selecting all entities
	Given a Context
	And a Repository
	When I call the GetAll method
	Then a entity collection should be returned

Scenario: Saving an Entity
	Given a mocked Context
	And a Repository
	And an Entity
	When I call the Save method on the Repository
	Then the Context Save method shold be called

Scenario: Deleting an Entity
	Given a mocked Context
	And a Repository
	And an Entity
	When I call the Delete method on the Repository
	Then the Context Delete method shold be called

Scenario: Deleting an Entity by it's Id
	Given a mocked Context
	And a Repository
	When I call the Delete by id method on the Repository
	Then the Context Delete by id method shold be called

Scenario: Selecting an entity using a Specification
	Given a Context
	And a Repository
	And a Specification
	When I call the GetFirstBySpecification method using the Specification
	Then a entity instance with the id 1 should be returned

Scenario: Selecting many entities using a Specification
	Given a Context
	And a Repository
	And a Specification
	When I call the GetBySpecification method using the Specification
	Then a entity collection should be returned