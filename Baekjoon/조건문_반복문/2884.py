import sys

#h, m = map(int,sys.stdin.readline().split())

h, m = 22, 40

if(m < 45):
    if h > 0 :
        h = h -1
    else:
         h = 23
    m =  m + 15
else:
    m = m -45
print(h,m)


import sys

h, m = map(int,sys.stdin.readline().split())

if m >= 45:
    print(h , m -45)
elif h > 0:
    print(h-1, m +15)
else:
    print(23, m + 15)