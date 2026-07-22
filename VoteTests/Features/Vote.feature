Feature: Vote

Simple Vote system

Scenario: 51% vs 49%
	Given candidates are
	| Candidates | Votes |
	| Ronald Plump | 49 |
	| Joe Widen | 51 |
	When vote ends
	Then the winner should be Joe Widen

Scenario: 50+% with many candidates
	Given candidates are
	| Candidates | Vote |
	| Emmanuel Valse | 0 |
	| Annie Dingo | 1 |
	| Manuel Macaron | 2 |
	| Citroën AMI | 97 |
	When vote ends
	Then the winner should be Citroën AMI

Scenario: 2 rounds
	Given candidates are
		| Candidates | Votes |
		| C++ | 40 |
		| C | 35 |
		| Rust | 20 |
		| Python | 5 |
		| Java | 0 |
	And candidates of the 2nd round are
		| Candidate | Votes |
		| C++ | 45 |
		| C | 55 |
	When vote ends
	Then the winner should be C
