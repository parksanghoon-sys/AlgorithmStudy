import sys

n = int(input())
data=[]
for i in range(n):
    data.append(int(input())) 

s = sorted(data,reverse=False)

for i in range(len(s)):
    print(s[i])
    
def binary_sort(arr):    
    if len(arr) <= 1:
        return arr
    pivot = arr[len(arr) //2]
    left, right, equal = [], [], []
    for i in arr:
        if i < pivot:
            left.append(i)
        elif i > pivot:
            right.append(i)
        else:
            equal.append(i)
    return binary_sort(left) + equal + binary_sort(right)
