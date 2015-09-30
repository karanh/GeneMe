# GeneMe
Console based Windows executable that targets bioinformatics applications for large datasets

A DNA sequence is made by combining the following 4 molecules.
1)	Adenine, represented by letter A.
2)	Cytosine, represented by letter C.
3)	Guanine, represented by letter G.
4)	Thymine, represented by letter T.


Takes in .txt files as input

1)	The computer program started by asking the user for a name of an input file as a string.  The file could be anything, such as DNA.txt, sequence.dat, etc.  This file would be used to open up a DNA sequence.  The file was read using a subprogram.

2)	The computer program asked for the name of an output file as a string.  This file could be anything, and would be used to output the results of the DNA analysis.

3)	The file format of a DNA sequence would start off with an integer, which indicates the number of lines of DNA were in the file.  It would look something like this:
4
A+A-T+T-T+T+T+T+T+T+C-C-G+G+A-
T-T+A-A-A-A-A-A-A-A-G+G+A-A-T+
C-G+T-A+C+A-
G+C-A+T-G-T+
4)	The computer program used an array of DNA structs to store all of the information in the file.  The struct would store both the molecule type, and the polarity (the + or -).  The DNA struct also implemented the IComparable interface with CompareTo so that they could be sorted.

5)	The computer program used a subprogram to analyse the information from the file to put it into an array of DNA structs.

6)	The computer program used a SelectionSort to sort all of the DNA structs BEFORE they were analysed for Rules 1 and 2.  They had to be SelectionSorted REVERSE alphabetically (T, G, C, then A).

7)	 The computer program used a MergeSort to sort all of the DNA structs BEFORE they were analysed for Rule 3.  They had to be MergeSorted REVERSE alphabetically (T, G, C, then A).

8)	The computer program used a MergeSort to sort all of the DNA structs BEFORE they were analysed for Rule 4.  They had to be MergeSorted by their polarity (All -, then all +).  Please write another subprogram in the DNA struct to compare 2 DNA structs by their polarity; you cannot use the same CompareTo() for sorting reverse alphabetically.

9)	The computer program used a MergeSort to sort the DNA REVERSE alphabetically, and then used a BinarySearch to search through all of the DNA structs for Rule 5 to determine if the mutant was able to shapeshift.

10)	The computer program used a SelectionSort to sort the DNA REVERSE alphabetically, and then used a LinearSearch to search through all of the DNA structs for Rule 6 to determine if the mutant had super-healing ability.

Mutants have 8 different DNA molecules:
A+, A-, C+, C-, G+, G-, T+, and T-.

MUTANT DNA RULES
Rule 1:
If there are more Thymine molecules than adenine, cytosine, and guanine combined, then the mutant is going to be evil.  If there are an equal number of T molecules to A, C, and G combined, the mutant is neutral.  Otherwise, the mutant will be good.

Rule 2:
If there is an even number of cytosine and guanine molecules combined, the mutant will have super strength.  Otherwise, the mutant will have normal strength.


Rule 3:
If there is an even number of adenine and thymine molecules combined, the mutant will have energy powers (like lightning, fire, etc.).  Otherwise, the mutant will not have any energy powers.

Rule 4:
If percentage of + molecules is greater than 80%, then the mutant will have psychic powers.  For example, if the mutant has 81 + molecules, and 19 – molecules, they are a psychic.  If the mutant has 40 + molecules and 10 – molecules, they are not psychic.

Rule 5:
	If there are no C- molecules, the mutant would be able to transform/shape shift.

Rule 6:
	If there are no A- molecules, the mutant would have super healing abilities.

