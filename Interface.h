#pragma once

/*********************Preprocessor Directives********************/
#include "Perceptron.h"

/*********************Global Variables********************/
Perceptron bot;

/*********************Dll Functions********************/
extern "C"
{
	_declspec(dllexport) void InitializePerceptron(short numberOfInputs, float threshold);
	_declspec(dllexport) bool Train(float* inputs, bool expectedOutput);
	_declspec(dllexport) bool GetOutput(float* inputs);
}

