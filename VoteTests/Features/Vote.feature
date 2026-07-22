Feature: Vote

Simple Vote for adding two numbers

@mytag
Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

@mytag
Scenario: Subtract two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are subtracted
	Then the result should be -20

@mytag:
Scenario: Multiply two numbers
	Given the first number is 5
	And the second number is 7
	When the two numbers are multiplied
	Then the result should be 35

Scenario: Divide two numbers
	Given the first number is 1
	And the second number is 0
	When the two numbers are divided
	Then the result should be 0
