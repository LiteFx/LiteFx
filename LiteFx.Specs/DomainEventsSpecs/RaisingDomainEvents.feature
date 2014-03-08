Feature: Rasing Domaind Events
	In order to alert all my domain
	As a developer
	I want to raise events against the model
	So that the event could be handled

Scenario: Rasing and handling a Domain Event
	Given I have registered a ordinary domain event handler into DomainEvent static class
	And a ordinary subject
	When I set a value in the ordinary subject 
	And the ordinary event happen
	Then my handler should be called

Scenario: Handling a Domain Event with an Action
	Given I have registered a ordinary action into DomainEvent static class
	And a ordinary subject
	When I set a value in the ordinary subject 
	And the ordinary event happen
	Then my action should be called

Scenario: Rasing a Domain Event Asynchronously
	Given I have registered a async domain event handler into DomainEvent static class
	And a ordinary subject
	When I set a value in the ordinary subject 
	And the ordinary event happen asynchronously
	Then my handler should be called
