using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibSVMsharp;
using LibSVMsharp.Helpers;

namespace DoSVMAnalyze
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("LibSVM Version =  v." + SVM.Version);

                Console.WriteLine("Load Training Data");
                SVMProblem problem = SVMProblemHelper.Load(@"D:/Gi/SignatureVerification/Tools/DoSVMAnalyze/DoSVMAnalyze/TrainingData.txt");
                if (problem == null)
                {
                    Console.WriteLine("Can Not Load Model");
                }

                Console.WriteLine("Load Testing Data");
                SVMProblem testProblem = SVMProblemHelper.Load(@"D:/Gi/SignatureVerification/Tools/DoSVMAnalyze/DoSVMAnalyze/TrainingData.txt");
                if (testProblem == null)
                {
                    Console.WriteLine("Can Not Load Test Model");
                }

                Console.WriteLine("Setting Parameter");
                SVMParameter parameter = new SVMParameter();
                parameter.Type = SVMType.C_SVC;
                parameter.Kernel = SVMKernelType.RBF;
                parameter.C = 1;
                parameter.Gamma = 1;

                Console.WriteLine("Training");
                SVMModel model = SVM.Train(problem, parameter);

                Console.WriteLine("Predicting");
                double[] target = new double[testProblem.Length];
                for (int i = 0; i < testProblem.Length; i++)
                {
                    target[i] = SVM.Predict(model, testProblem.X[i]);
                }

                Console.WriteLine("Calculating Accuracy");
                double accuracy = SVMHelper.EvaluateClassificationProblem(testProblem, target);
      
            }
            catch (Exception e) {
                Console.WriteLine("Error!!");
                Console.WriteLine("Message = " + e.Message);
            }
            Console.Read();
        }
    }
}
