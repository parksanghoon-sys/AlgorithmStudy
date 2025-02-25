import sys

bigPaper = [[False for _ in range(100)] for _ in range(100)]

count = (int(input()))

for i in range(count):
    n, m = map(int, input().split())
    for i in range(10):
        for j in range(10):        
            bigPaper[n + i][m +j] = True

true_count = sum(row.count(True) for row in bigPaper)
print(true_count)
