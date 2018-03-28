
/*********************Preprocessor Directives********************/
#include "stdafx.h"
#include "Interface.h"

/*********************Dll Functions********************/
void InitializePerceptron(short numberOfInputs, float threshold)
{
	//Initializing the bot
	bot = Perceptron(numberOfInputs, threshold);
}

bool Train(float* inputs, bool expectedOutput)
{
	std::vector<float> inputVec;

	//Storing inputs in a vector
	short numInputs = bot.NumberOfInputs();
	for (int a = 0; a < numInputs; ++a)
	{
		inputVec.push_back(inputs[a]);
	}

	//Training the bot
	return bot.Train(inputVec, expectedOutput);
}

bool GetOutput(float* inputs)
{
	std::vector<float> inputVec;

	//Storing inputs in a vector
	short numInputs = bot.NumberOfInputs();
	for (int a = 0; a < numInputs; ++a)
	{
		inputVec.push_back(inputs[a]);
	}

	return bot.Output(inputVec);
}