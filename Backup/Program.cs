/*Karan Huynh
 *April 7th, 2013
 *Reads in a DNA file, anaylizes the information given
 *to see characteristics of the mutant, then prints it out to a file*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AssignmentTwo
{
    class Program
    {
        //Stores the text that gets outputted to the file
        public static string[] fileOuputText = new string[6];
        static void Main(string[] args)
        {
            //Gets the file input name
            string fileName = GetFileInputName();
            //Gets the file output name
            string fileOutputName = GetFileOutputName();
            //Gets the entire DNA sequence from the file as one string
            string fileInfo = ReadInFile(fileName);
            //Used to store the DNA sequecnce as individual chars
            char[] DNACharArray = new char[fileInfo.Length];
            //Stores the values from the fileInfo as chars in an array
            DNACharArray = GetCharArray(fileInfo, DNACharArray);
            //Used to store the file information as a DNA struct array
            DNA[] DNASequence = new DNA[(DNACharArray.Length) / 2];
            //Assigns the charges and bases to the DNASequence from the DNAChararray
            MergeBaseAndCharge(DNASequence, DNACharArray);
            //Sorts the DNASequence array by base type reverse alphabetically using selection sort
            SelectionSortDNAReverse(DNASequence);
            //Checks the personality of the mutant
            RuleOne(DNASequence);
            //Checks the strength of the mutant
            RuleTwo(DNASequence);
            //Sorts the DNASequence array by base type reverse alphbetically using merge sort
            DNASequence = MergeSortBase(DNASequence, 0, DNASequence.Length - 1);
            //Checks if the mutant has energy powers
            RuleThree(DNASequence);
            //Sorts the DNASequence array by base type reverse alphbetically by merge sort
            DNASequence = MergeSortCharge(DNASequence, 0, DNASequence.Length - 1);
            //Checks if the mutant has psychic powers or not
            RuleFour(DNASequence);
            //Sorts the DNASequence array by charge with all negatives charges followed by all positives charges using merge sort
            DNASequence = MergeSortBase(DNASequence, 0, DNASequence.Length - 1);
            //Declares DNA datatype for value that makes it not be able to shape shift
            DNA findValue1;
            //Stores the values of the value that makes it not be able to shape shift
            findValue1.Base = 'C';
            findValue1.Charge = '-';
            //Checks if the mutant can shape shift using binary search
            int checkShapeShift = BinarySearch(DNASequence, 0, DNASequence.Length - 1, findValue1);
            //Mutant cannot shape shift
            if (checkShapeShift == 1)
            {
                //Saved in array so analysis can be written to file
                fileOuputText[4] = "No shapeshifting";
            }
            //Mutant can shape shift
            else if (checkShapeShift == 0)
            {
                //Saved in array so analysis can be written to file
                fileOuputText[4] = "Shape-shifter";
            }
            //Sorts the DNASequence array by base type reverse alphabetically using selection sort
            SelectionSortDNAReverse(DNASequence);
            //Declares DNA datatype for value that makes the mutant not be able to super heal
            DNA findValue2;
            //Stores the values that makes the mutant not be able to super heal
            findValue2.Base = 'A';
            findValue2.Charge = '-';
            //Checks if the mutant can shape shift 
            int checkHealingAbility = LinearSearch(DNASequence, findValue2);
            //Mutant cannot super heal
            if (checkHealingAbility == 1)
            {
                //Saved in array so analysis can be written to file
                fileOuputText[5] = "No super-healing";
            }
            //Mutant can super heal
            else if (checkHealingAbility == 0)
            {
                //Saved in array so analysis can be written to file
                fileOuputText[5] = "Super-healing";
            }
            //Writes the characteristics of the mutant to the file
            WriteToFile(fileOutputName);
            //Tells user the file has been written
            Console.WriteLine("File has been written to a .txt file");
        }
        //Writes the characteristics of the mutant to a .txt file
        //Takes in user specified file output name
        static void WriteToFile(string fileOutputName)
        {
            //Opens up the file to write to
            using (StreamWriter fileOutput = new StreamWriter(fileOutputName))
            {
                //Writes the characteristics of the mutant to the file using a for loop from the information stored in the fileOutputText array
                for (int i = 0; i < fileOuputText.Length; i++)
                {
                    fileOutput.WriteLine(fileOuputText[i]);
                }
            }
        }
        //Gets the file input name from the user
        //Checks if it exists, if not, prompts the user to enter one that does exist
        //Returns the file input name
        static string GetFileInputName()
        {
            //Used to check if the file is found
            bool checkFileExists;
            //Used to store the file input name
            string fileName;
            //Runs until the file is found
            do
            {
                //Prompts user to input a file name
                Console.WriteLine("Please enter a file input name along with the correct file extention");
                //Gets the file name
                fileName = Console.ReadLine();
                //Stores true if the file to be loaded is there and false if not
                checkFileExists = File.Exists(fileName);
                //The file does not exists
                if (checkFileExists == false)
                {
                    //Warning message
                    Console.WriteLine("ERROR. The file does not exists");
                    //File checking resets
                    checkFileExists = true;
                }
                //File is found 
                else
                {
                    //Returns the file name
                    return fileName;
                }
            //Exits the loop once the file is found
            } while (checkFileExists != false);
            //Returns the name of the input file
            return fileName;
        }
        //Gets the file output name
        //Checks if it exists, if does, prompts to enter to overwrite or enter a name of a file that doesnt exists
        //Returns the file output name
        static string GetFileOutputName()
        {
            //Used to check if the file exists
            bool checkFileExists;
            //Used to store file output name
            string fileName;
            //Runs while the file name does not already exist
            do
            {
                //Prompts user to enter a file output name
                Console.WriteLine("Please enter a file output name");
                //Console.WriteLine("Please enter a file output extension");
                //Stores the file input name with a .txt file extension
                fileName = Console.ReadLine()+".txt";
                //Checks if file already exists
                checkFileExists = File.Exists(fileName);
                //File already exists
                if (checkFileExists == true)
                {
                    //User warning and asks if the file wants to over written
                    Console.WriteLine("ERROR. The file already exists. Would you like to overwrite the file? Y or N");
                    //Used to store if user wants to overwrite a file
                    string overwrite;
                    //Runs while the user wishes to overwrite a file or not
                    do
                    {
                        //Stores the user wants to overwrite existing file
                        overwrite = Console.ReadLine();
                        //Returns existing file name to be overwritten
                        if (overwrite == "Y")
                        {
                            return fileName;
                        }
                        //User does not want the file to be overwritten and breaks the loop so that the user can be asked to enter a few file name
                        else if (overwrite == "N")
                        {
                            //File does ont exist
                            checkFileExists = false;
                            //Exists do while loop
                            break;
                        }
                        //User did not enter a valid answer
                        else
                        {
                            //User warning
                            Console.WriteLine("Invalid response. Would you like to over write the file? Y or N");
                        }
                    //Runs until the user enters valid response
                    } while (overwrite != "Y" || overwrite != "N");
                }
                //File name does not exists and returns file name
                else
                {
                    return fileName;
                }
            //Runs while the file name does not already exist
            } while (checkFileExists != true);
            //Returns the file output name
            return fileName;
        }
        //Searches through DNASequence array to see if A- is in the sequence
        //Returns 1 if A- is present and 0 if not
        //Uses linear search to search for A- element
        static int LinearSearch(DNA[] DNASequence, DNA findValue)
        {
            //Checks if A- is found
            int found = 0;
            //Looks at each element in the DNASequence array individually to see if A- is in the sequence
            for (int i = 0; i < DNASequence.Length; i++)
            {
                //Checks if A- is in the DNA sequence 
                if (DNASequence[i].Base == findValue.Base && DNASequence[i].Charge == findValue.Charge)
                {
                    //A- is found
                    found = 1;
                }
            }
            //Returns either 0 for not found and 1 if found
            return found;
        }
        //Searches through DNASequence array to see if C- is in the sequence
        //Returns 1 if C- if found and 0 if not
        //Uses binary search recursively to find C-
        //Takes in DNASequence, 0 as the lowestIndex, the highest index minus one as the largestIndex, and C- as the value to find
        static int BinarySearch(DNA[] DNASequence, int lowestIndex, int largestIndex, DNA findValue)
        {  
            //Gets the middle index value
            int middleIndex = (lowestIndex + largestIndex) / 2;

            //The array is empty and value to find is not in the sequence and returns 0
            if (lowestIndex > largestIndex)
            {
                return 0;
            }
            //The value to find is in the first half of the array to search 
            //Middle element is larger than C
            else if (DNASequence[middleIndex].CompareTo(findValue) < 0 )
            {
                //Call BinarySearch and searches through only the first half
                return BinarySearch(DNASequence, 0, middleIndex - 1, findValue);
            }
            //The value to find is in the second half of the array to search
            //Middle element is smaller than C
            else if (DNASequence[middleIndex].CompareTo(findValue) > 0 )
            {
                //Call BinarySearch and searches through only the second half
                return BinarySearch(DNASequence, middleIndex + 1, largestIndex, findValue);
            }
            //The value to find is in the first half of the array to search 
            //Middle element has a C base but a + charge 
            else if (DNASequence[middleIndex].CompareTo(findValue) == 0 && DNASequence[middleIndex].CompareChargeTo(findValue) < 0)
            {
                //Call BinarySearch and searches through only the second half in case there is a C with a - charge
                return BinarySearch(DNASequence, middleIndex + 1, largestIndex, findValue);
            }
            //The value you to find is the middle element
            else if (DNASequence[middleIndex].CompareTo(findValue) == 0 && DNASequence[middleIndex].CompareChargeTo(findValue) == 0)
            {
                //C- is in the DNA sequence
                return 1;
            }
            //C- is not found and returns 0
            else
            {
                return 0;
            }
        }
        //Takes in the DNASequence array and counts the number of postive and negative charges
        //If there are more than 80% positive charges, mutant is psychic, if not, it is not psychic
        static void RuleFour(DNA[] DNASequence)
        {
            //Counts every positive charge
            int positiveChargeCount = 0;
            //Counts every negative charge 
            int negativeChargeCount = 0;
            //Counts for the length of the DNASequence array
            for (int i = 0; i < DNASequence.Length; i++)
            {
                //Checks if element is a - charge
                if (DNASequence[i].Charge == '-')
                {
                    //Add 1 to the count
                    negativeChargeCount++;
                }
                //Checks if element is a + charge
                else
                {
                    //Add 1 to the count
                    positiveChargeCount++;
                }
            }
            //Checks if there are more than 80% positive charges, if so, it is psychic
            if ((positiveChargeCount / (positiveChargeCount + negativeChargeCount)) * 100 > 80)
            {
                //Saved in array so analysis can be written to file
                fileOuputText[3] = "Psychic";
            }
            //Mutant is not psychic
            else
            {
                //Saved in array so analysis can be written to file
                fileOuputText[3] = "Not Psychic";
            }
        }
        //Sorts the DNASequence by base using merge sort
        //Takes in the DNASequence, and the upper and lower bounds of the array
        static DNA[] MergeSortCharge(DNA[] DNASequence, int smallestIndex, int largestIndex)
        {
            /* Stop the recursive calls when we are looking
            * at an array of size 1. */
            if (largestIndex == smallestIndex)
            {
                //The array is already sorted with an element of one
                DNA[] DNASequenceMergeSort = new DNA[1];
                DNASequenceMergeSort[0] = DNASequence[smallestIndex];
                return DNASequenceMergeSort;
            }
            /* Just in case, if the largestIndex is bigger
             * than the smallestIndex, return nothing. */
            else if (largestIndex < smallestIndex)
            {
                return null;
            }
            /* If the array is larger than size 1,
             * perform the MergeSort
             */
            else
            {
                /* Step 1: Divide the problem into
                 * 2 halves */
                int middleIndex = (smallestIndex + largestIndex) / 2;
                /* Merge sorts the first half and the second half of the
                 *array
                 *creates temproary arrays to hold the first and second half of the array*/
                DNA[] firstHalf = MergeSortCharge(DNASequence, smallestIndex, middleIndex);
                DNA[] secondHalf = MergeSortCharge(DNASequence, middleIndex + 1, largestIndex);
                /* Just in case, if the first half of the array is null
                 * just return the first half of the array. */
                if (firstHalf == null)
                {
                    return secondHalf;
                }
                /* Just in case, if the second half of the array is null
                * just return the first half of the array. */
                else if (secondHalf == null)
                {
                    return firstHalf;
                }
                /* Keep track of the indexes for each half of
                 * the arrays.  */
                int firstHalfIndex = 0;
                int secondHalfIndex = 0;
                /* Create a temporary storage array for combining
                 * the 2 sorted halves */
                DNA[] mergedHalves = new DNA[firstHalf.Length + secondHalf.Length];
                /* Count through all indexes in the combinedHalves
                 * array to finish merging the 2 halves */
                for (int i = 0; i < mergedHalves.Length; i++)
                {
                    /* If the current element in the first half of the
                    * array is smaller than the element in the second
                    * half of the array, put it in the combinedHalves
                    * array. Calls the struct compareToCharge that compares the charges */
                    if (firstHalf[firstHalfIndex].CompareChargeTo(secondHalf[secondHalfIndex]) > 0)
                    {
                        /* Put the element in the first half of the array
                         * into the combinedHalves array, and increase
                         * the current index for the firstHalf array */
                        mergedHalves[i] = firstHalf[firstHalfIndex];
                        firstHalfIndex++;
                        /* If we are done the first half of the array
                         * copy the rest of the second half right away
                         */
                        if (firstHalfIndex == firstHalf.Length)
                        {
                            /* Go through all remaining indexes in the
                             * secondHalf array to copy them */
                            for (int index = i + 1; index < mergedHalves.Length; index++)
                            {
                                /* Copy from secondHalf array into the
                                 * combined array, and increase the
                                 * index so that the next loop will get
                                 * the next element from secondHalf */
                                mergedHalves[index] = secondHalf[secondHalfIndex];
                                secondHalfIndex++;
                            }
                            // Set i so that the for loop will exit
                            i = mergedHalves.Length;
                        }
                    }
                    /* Otherwise, the element in the second half of the
                    * array gets put put it in the combinedHalves
                    * array. */
                    else
                    {
                        /* Put the element in the second half of the arra
                         * into the combinedHalves array, and increase
                         * the current index for the secondHalf array */
                        mergedHalves[i] = secondHalf[secondHalfIndex];
                        secondHalfIndex++;
                        /* If we are done the second half of the array
                         * copy the rest of the first half right away
                         */
                        if (secondHalfIndex == secondHalf.Length)
                        {
                            /* Go through all remaining indexes in the
                             * firstHalf array to copy them */
                            for (int index = i + 1; index < mergedHalves.Length; index++)
                            {
                                /* Copy from firstHalf array into the
                                 * combined array, and increase the
                                 * index so that the next loop will get
                                 * the next element from firstHalf */
                                mergedHalves[index] = firstHalf[firstHalfIndex];
                                firstHalfIndex++;
                            }
                            // Set i so that the for loop will exit
                            i = mergedHalves.Length;
                        }
                    }
                }
                //returns the sorted array
                return mergedHalves;
            }
        }
        //Checks if the mutant will have energy powers or not by anazying the information taken in frm the DNASequence array
        //If there is an even number of adnenine and thymine then the mutant has energy powers
        //If not, the mutant does not have energy powers
        static void RuleThree(DNA[] DNASequence)
        {
            //Used to count the number number of adenine and thymine in the sequence
            int adenineThymineCount = 0;
            //Runs through the DNASequence using a for loop to check if any elements are thymine or 
            for (int i = 0; i < DNASequence.Length; i++)
            {
                //The element in the array is either a T or A
                if (DNASequence[i].Base == 'T' || DNASequence[i].Base == 'A')
                {
                    //Add one to the count
                    adenineThymineCount = adenineThymineCount + 1;
                }
            }
            //Console.WriteLine(adenineThymineCount);
            if (adenineThymineCount % 2 == 0)
            {
                //Saved in array so analysis can be written to file
                fileOuputText[2] = "Energy";
            }
            else
            {
                //Saved in array so analysis can be written to file
                fileOuputText[2] = "No Energy";
            }
        }
        //Sorts the DNASequence by base using merge sort
        //Takes in the DNASequence, and the upper and lower bounds of the array
        static DNA[] MergeSortBase(DNA[] DNASequence, int smallestIndex, int largestIndex)
        {
            /* Stop the recursive calls when we are looking
             * at an array of size 1. */
            if (largestIndex == smallestIndex)
            {
                //The array is already sorted with an element of one
                DNA[] DNASequenceMergeSort = new DNA[1];
                DNASequenceMergeSort[0] = DNASequence[smallestIndex];
                return DNASequenceMergeSort;
            }
            /* Just in case, if the largestIndex is bigger
             * than the smallestIndex, return nothing. */
            else if (largestIndex < smallestIndex)
            {
                return null;
            }
            /* If the array is larger than size 1,
             * perform the MergeSort
             */
            else
            {
                /* Step 1: Divide the problem into
                 * 2 halves */
                int middleIndex = (smallestIndex + largestIndex) / 2;
                /* MergeSort the first half and the second half of the
                 *array
                 *creates temproary arrays to hold the first and second half of the array*/
                DNA[] firstHalf = MergeSortBase(DNASequence, smallestIndex, middleIndex);
                DNA[] secondHalf = MergeSortBase(DNASequence, middleIndex + 1, largestIndex);
                /* Just in case, if the first half of the array is null
                 * just return the first half of the array. */
                if (firstHalf == null)
                {
                    return secondHalf;
                }
                /* Just in case, if the second half of the array is null
                * just return the first half of the array. */
                else if (secondHalf == null)
                {
                    return firstHalf;
                }
                /* Keep track of the indexes for each half of
                 * the arrays.  */
                int firstHalfIndex = 0;
                int secondHalfIndex = 0;
                /* Create a temporary storage array for combining
                 * the 2 sorted halves */
                DNA[] mergedHalves = new DNA[firstHalf.Length + secondHalf.Length];
                /* Count through all indexes in the combinedHalves
                 * array to finish merging the 2 halves */
                for (int i = 0; i < mergedHalves.Length; i++)
                {
                    /* If the current element in the first half of the
                    * array is smaller than the element in the second
                    * half of the array, put it in the combinedHalves
                    * array. Checks if the base is greater or smaller by using the CompareTo that sorts by base*/
                    if (firstHalf[firstHalfIndex].CompareTo(secondHalf[secondHalfIndex]) > 0)
                    {
                        /* Put the element in the first half of the array
                         * into the combinedHalves array, and increase
                         * the current index for the firstHalf array */
                        mergedHalves[i] = firstHalf[firstHalfIndex];
                        firstHalfIndex++;
                        /* If we are done the first half of the array
                         * copy the rest of the second half right away
                         */
                        if (firstHalfIndex == firstHalf.Length)
                        {
                            /* Go through all remaining indexes in the
                             * secondHalf array to copy them */
                            for (int index = i + 1; index < mergedHalves.Length; index++)
                            {
                                /* Copy from secondHalf array into the
                                 * combined array, and increase the
                                 * index so that the next loop will get
                                 * the next element from secondHalf */
                                mergedHalves[index] = secondHalf[secondHalfIndex];
                                secondHalfIndex++;
                            }
                            // Set i so that the for loop will exit
                            i = mergedHalves.Length;
                        }
                    }
                    /* Otherwise, the element in the second half of the
                    * array gets put put it in the combinedHalves
                    * array. */
                    else
                    {
                        /* Put the element in the second half of the arra
                         * into the combinedHalves array, and increase
                         * the current index for the secondHalf array */
                        mergedHalves[i] = secondHalf[secondHalfIndex];
                        secondHalfIndex++;
                        /* If we are done the second half of the array
                         * copy the rest of the first half right away
                         */
                        if (secondHalfIndex == secondHalf.Length)
                        {
                           /* Go through all remaining indexes in the
                            * firstHalf array to copy them */
                            for (int index = i + 1; index < mergedHalves.Length; index++)
                            {
                                /* Copy from firstHalf array into the
                                 * combined array, and increase the
                                 * index so that the next loop will get
                                 * the next element from firstHalf */
                                mergedHalves[index] = firstHalf[firstHalfIndex];
                                firstHalfIndex++;
                            }
                            // Set i so that the for loop will exit
                            i = mergedHalves.Length;
                        }
                    }
                }
                //returns the sorted array
                return mergedHalves;
            }
        }                   
        //Checks if the mutant has super strength or not
        //Takes in the DNASequence file and counts the number of elements that are cytoine or guanine
        //If there is an even number of cytosine and guanine bases combined, then the mutant has super powers, if not, it will not have super poweres
        static void RuleTwo(DNA[] DNASequence)
        {
            //Used to count the nuber of guanine and cytosine elements in the sequence
            int cytosineAndGuanineCount = 0;
            //Runs for the length of the DNASequece array
            for (int i = 0; i < DNASequence.Length; i++)
            {
                //An element in the array is either cytosine or guanine
                if (DNASequence[i].Base == 'C' || DNASequence[i].Base == 'G')
                {
                    //Add one to the cytosine and guanine count
                    cytosineAndGuanineCount = cytosineAndGuanineCount + 1;
                }
            }
            //Console.WriteLine(cytosineAndGuanineCount);
            if (cytosineAndGuanineCount % 2 == 0)
            {

                //Saved in array so analysis can be written to file
                fileOuputText[1] = "Super strength";
            }
            else
            {
                //Saved in array so analysis can be written to file
                fileOuputText[1] = "Normal strength";
            }

        }
        //Checks the personality of the mutant
        //Takes in the DNASequence and counts the number of thymine elements in the array
        //If there are more thymine bases than guanine, cytosine, and adenine combined, then the mutant is evil, if the amount is 
        //the same then the mutant is neutral, if there are less thymine then the rest of the bases combined the mutant is good
        static void RuleOne(DNA[] DNASequence)
        {
            //Counts the number of thymine elements in the array
            int thymineCount = 0;
           //Counts the number of guanine, cytosine, and adenine elements in the array
            int notThymineCount = 0;
            //Runs for the length of the DNASequence array
            for (int i = 0; i < DNASequence.Length; i++)
            {
                //An element in the array is thymine
                if (DNASequence[i].Base == 'T')
                {
                    //Adds one to the count
                    thymineCount = thymineCount + 1;
                }
                //Element in the array is not thymine
                else
                {
                    //Adds one to the count
                    notThymineCount = notThymineCount + 1;
                }
            }
            //Compares the number of thymine to the number of non thymine elements
            //The mutant is good 
            if (thymineCount > notThymineCount)
            {
                //Saved in array so analysis can be written to file
                fileOuputText[0] = "Evil";
            }
            //The mutant is neutral
            else if (thymineCount == notThymineCount)
            {
                //Saved in array so analysis can be written to file
                fileOuputText[0] = "Neutral";
            }
            //The mutant is good
            else
            {
                //Saved in array so analysis can be written to file
                fileOuputText[0] = "Good";
            }
        }
        //Takes in the DNACharArray and assigns its element it to the DNA struct array, DNASequence with corresponding element values
        //Groups the nitogen base to its correcsponding charge by using the DNASequence array
        static void MergeBaseAndCharge(DNA[] DNASequence, char[] DNACharArray)
        {
            //Used for an even index count
            int evenIndex = 0;
            //Used for an odd index count
            int oddIndex = 1;
            //Runs for half the length of the DNACharArray to assign all the values to he DNASequence array
            for (int index = 0; index < (DNACharArray.Length) / 2; index++)
            {
                //The DNASequence will only store every base in the DNACharArray
                //Each base is found on every even index of the DNACharArray and assigns it to the DNASequence array
                DNASequence[index].Base = DNACharArray[evenIndex];
                //The DNASequence will only store every charge in the DNACharArray
                //Each charge is found on every odd index of the DNACharArray and asigns it to the DNASequence array
                DNASequence[index].Charge = DNACharArray[oddIndex];
                //Adds two the count to get the next even value
                evenIndex = evenIndex + 2;
                //Adds tso the count to get the next odd value
                oddIndex = oddIndex + 2;
            }
        }
        //Takes in the DNASequence array and sorts by selection sort
        //The bases are sorted reverse alphabetically where the largest base element if in the front
        static void SelectionSortDNAReverse(DNA[] DNASequence)
        {
            //Counts through the length of the DNASequence - 1 because the last index is already sorted
            //Runs forthe length of the DNASequence
            for (int i = 0; i < DNASequence.Length - 1; i++)
            {
                //Used to stores the current largest element
                int position = i;
                //Counts through the indexes to find the smallest element
                for (int index = position + 1; index < DNASequence.Length; index++)
                {
                    //Uses the DNA struct CopareTo to compare the bases to find a smaller bigger
                    //base value
                    //Larger base value is found
                    if (DNASequence[index].CompareTo(DNASequence[position]) > 0)
                    {
                        //Current largest element is saved to futher compare for larger elements
                        position = index;
                    }
                }
                //Swaps the largest element to the front of the array
                //by temporarily storing it in a temp DNA variable
                DNA temp = DNASequence[position];
                //Moves the first element to where the index of the largest element used ot be
                DNASequence[position] = DNASequence[i];
                //Moves the largest element to the front of the array
                DNASequence[i] = temp;
            }
        }
        //Takes in the file information as a giant string and breaks it into a char array
        //Returns it as an array of chars that contain the individual charges and bases
        static char[] GetCharArray(string fileInfo, char[] DNACharArray)
        {
            
            //Stores the individual characters of the DNA sequence
            DNACharArray = fileInfo.ToCharArray();
            return DNACharArray;
        }
        //Reads in the file name from the user and checks if it is an existing file
        //Opens the file for anaylisis if the file exists
        //Prompts user to input a correct file name if the file does not is exists
        static string ReadInFile(string fileName)
        {
            //Opens up file for loading
            using (StreamReader fileInput = new StreamReader(fileName))
            {
                //Used to store the information in the file as a giant string
                string fileLine = "";
                //Stores the number of lines in the file
                string runLength;
                //
                int runLengthNumber;

                //Reads in the first line of the file the determines the number of lines the DNA seqence is found in the file
                runLength = fileInput.ReadLine();
                //Checks if the first line is a number
                bool numberOfLines = int.TryParse(runLength, out runLengthNumber);
                //The first line is a number
                if (numberOfLines == true)
                {
                    //Runs for the length of the number of lines that contain the DNA sequence in the file
                    for (int index = 0; index < runLengthNumber; index++)
                    {
                        //Adds the next line that contains the DNA seqence
                        fileLine = fileLine + fileInput.ReadLine();
                    }
                }
                //Returns the DNA sequence in the file as a giant string
                return fileLine;
            }
        }
        //Struct created to compare the nitogen bases and the charges of the DNA sequence
        //Used for the anaysis of the DNA sequence
        struct DNA: IComparable
        {
            //Stores the nitogen base of the DNA sequence as one of A, C, T, or G
            public char Base;
            //Stores the charge of the nigtrogen base as either + or -
            public char Charge;
            //Allows us to compare DNA stuct datatypes for the nitrogen bases
            //Returns -1 if the base element value passed in is greater, 0 if the value 
            //1 if base element value passed in is smaller
            public int CompareTo(object obj)
            {
               //Checks if the parameter passed in is a DNA datatype
                if (obj is DNA)
                {
                    //Converts the parameter from a object to a DNA datatype to be used for comparing
                    DNA compareDNA = (DNA)obj;
                    //The base value of the parmeter passed in comes after the base in the alphabet
                    if (Base < compareDNA.Base)
                    {
                        return -1;

                    }
                    //The base value of the parameter passed in comes before the base in the alphabet
                    else if (Base > compareDNA.Base)
                    {
                        return 1;
                    }
                    //The base values are the same
                    else
                    {
                        return 0;
                    }
                }
                //The parameter passed in is not a DNA datatype
                else
                {
                    //User warning
                    throw new Exception("Not a DNA datatype");
                }
            }
            //Allows us to compare DNA stuct datatypes of the charges
            //A + charge is greather than a - charge
            //Returns -1 if the charge element passed less than the charge value, 0 if they are the same charge 
            //1 if base element value passed in is greater than the charge value
            public int CompareChargeTo(object parameter)
            {
                //Checks if the paramter passed in is a DNA datatype
                if (parameter is DNA)
                {
                    //Converts the parameter from a object datatype to a DNA datatype
                    DNA compareCharge = (DNA)parameter;
                    //The charge passed in is smaller than the charge it's compared to
                    if (Charge == '+' && compareCharge.Charge == '-')
                    {
                        return -1;
                    }
                    //The charges are the same
                    else if (Charge == compareCharge.Charge)
                    {
                        return 0;
                    }
                    //The charge passed in is greater than the charge it's compared to
                    else
                    {
                        return 1;
                    }

                }
                //The parameter passed in is not a DNA datatype
                else
                {
                    //User warning
                    throw new Exception("Not a DNA datatype");
                }
            }
        }

    }
}
