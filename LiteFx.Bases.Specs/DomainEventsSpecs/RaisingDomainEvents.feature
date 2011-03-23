Feature: Rasing Domaind Events
	In order to alert all my domain
	As a developer
	I want to raise events against the model
	So that anyone could handle it

Scenario: Rasing and handling a Domain Event
	Given I have registered a ordinary domain event into DomainEvent static class
	When the ordinary event happen
	Then my handler should be called
