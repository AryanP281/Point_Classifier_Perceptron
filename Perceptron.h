#pragma once

/*********************Preprocessor Directives********************/
#include<vector>

/*********************Class********************/
class Perceptron
{
private :
	float learningRate; //The learning rate for the perceptron
	float threshold; //The threshold for the perceptrons output
	std::vector<float> weights; //The weights to the perceptron

	void BackPropogate(const std::vector<float>& inputs, bool guess, bool expectedOutput);/*
	Adjusts the weights*/

public:
	//Constructors And Destructors
	Perceptron();
	Perceptron(short numberOfInputs, float threshold);
	~Perceptron();

	//Access Methods
	short NumberOfInputs() const; //Returns the number of inputs the perceptron has

	//Methods
	bool Train(const std::vector<float>& input, bool expectedOutput);
	bool Output(const std::vector<float>& input);

	//Operators
	void operator =(const Perceptron& p);
};