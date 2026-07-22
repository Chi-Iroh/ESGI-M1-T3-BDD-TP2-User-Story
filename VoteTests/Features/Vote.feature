Feature: Vote

Simple Vote system

Scenario: 51% vs 49%
	Given candidates are
	| Candidates | Votes |
	| Ronald Plump | 49 |
	| Joe Widen | 51 |
	When vote ends
	Then the winner should be Joe Widen
