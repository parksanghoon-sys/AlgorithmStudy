import sys

# n = (int(input()))


n = 1900

if (n % 100 == 0 and n % 400 != 0):
    print(0)
elif(n % 4 == 0):
    print(1)
else:
    print(0)