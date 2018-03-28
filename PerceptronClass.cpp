
/*********************Preprocessor Directives********************/
#include "stdafx.h"
#include <random>
#include <time.h>
#include "Perceptron.h"

/*********************Constructors And Destructors********************/
Perceptron::Perceptron()
{
}

Perceptron::Perceptron(short numberOfInputs, float threshold)
{
	//Initializing the weights
	srand(time(NULL));
	for (int a = 0; a < numberOfInputs; ++a)
	{
		weights.push_back((float)rand() / (float)RAND_MAX);
	}

	//Initializing the threshold
	this->threshold = threshold;

	//Initializing learning rate
	learningRate = 0.35f;
}

Perceptron::~Perceptron()
{
}

/*********************Private Methods********************/
void Perceptron::BackPropogate(const std::vector<float>& inputs, bool guess, bool expectedOutput)
{
	char error = expectedOutput - guess; //The error in the perceptron's guess

	//Adjusting the weights
	for (int a = 0; a < weights.size(); ++a)
	{
		weights[a] += learningRate * inputs[a] * error;
	}
}

/*********************Access Methods********************/
short Perceptron::NumberOfInputs() const
{
	return weights.size();
}

/*********************Methods********************/
bool Perceptron::Train(const std::vector<float>& inputs, bool expectedOutput)
{
	bool guess = Output(inputs);

	BackPropogate(inputs, guess, expectedOutput);

	return guess;
}

bool Perceptron::Output(const std::vector<float>& inputs)
{
	float sum = 0.0f;
	
	//Calculating the weighted sum of inputs
	for (int a = 0; a < weights.size() - 1; ++a)
	{
		sum += inputs[a] * weights[a];
	}

	if (sum < threshold)
		return 0;
	else if (sum >= threshold)
		return 1;
}

/*********************Operators********************/
void Perceptron::operator=(const Perceptron& p)
{
	this->weights = p.weights;
	this->threshold = p.threshold;
	this->learningRate = p.learningRate;
}