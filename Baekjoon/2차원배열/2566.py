import sys


array = [[0 for col in range(9)] for row in range(9)]

for i in range(9):    
    num_list = list(map(int,sys.stdin.readline().split()))
    array[i] = num_list

maxinum = 0
row, col = 0,0

for i in range(len(array)):
    for j in range(len(array[i])):
        d = array[i][j]
        if maxinum <= d:
            maxinum = array[i][j]
            row, col = i +1 ,j +1            
    
    
print(maxinum)
print (row, col)