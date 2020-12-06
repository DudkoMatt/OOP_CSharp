#!/bin/bash

if [[ $# -ne 1 ]]
then
	echo 'Usage: ./script.bash <dir>'
	exit
fi

if [[ ! -d "$1" ]]
then
	echo "Directory $1 doesn't exist"
	exit
fi

cd "$1"

if [[ -d output ]]
then
	echo -n "Erase output?: [Y/n] "
	read CMD
	if [[ $CMD == "Y" || $CMD == "y" ]]
	then
		rm -rf output
	fi
fi

mkdir output

k=0
files=$(find . -type f -name "*.cs" | grep -vE "(./obj|./bin|./output)")
for i in $files
do
	cp "$i" ./output/
	k=$(($k+1))
done

cd ./output/
touch Отчет_Ход_рассуждений.txt
printf "В ходе написания данной лабораторной работы БЫЛ СОЗДАН $k ФАЙЛ с с исходным кодом на языке C#.\n\n" > Отчет_Ход_рассуждений.txt

k=1
for i in $files
do
	printf "$k) $(basename $i)\n\n" >> Отчет_Ход_рассуждений.txt
	k=$(($k+1))
done

printf "\n\nВ функции Main приведены примеры использования данных классов. Все исходные файлы приложены к лабораторной работе." >> Отчет_Ход_рассуждений.txt