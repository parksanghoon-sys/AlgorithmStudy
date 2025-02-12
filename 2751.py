import sys

n = int(input())
data=[]
for i in range(n):
    data.append(int(input())) 

s = sorted(data,reverse=False)

for i in range(len(s)):
    print(s[i])
