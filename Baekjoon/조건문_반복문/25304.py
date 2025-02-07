import sys

total = (int(input()))

model_count = int(input())

for i in range(model_count):
    price, count = map(int, input().split())
    for j in range(count):
        total = total - price

if total == 0:
    print("Yes")
else:
    print("No")