# input 활용

### 값 한개만 받는 경우
# 문자열 한 개 입력받기.
my_str = input()

# 정수 한 개 입력받기
my_num = int(input())


### 한 줄에 값 여러개 입력 받기

# input: abc def ghi
# output: ["abc", "def", "ghi"]
num_list = list(map(str, input().split()))

# input: 1 2 3
# output: [1, 2, 3]
num_list = list(map(int, input().split()))
a, b, c = map(int, input().split())


### 한줄당 문자열 한개, 여러줄 입력 받기

# 줄 개수는 n 이라 가정.
str_list = [input() for _ in range(n)]



# sys.stdin.readline 를 활용하자.

### 값 한개만 받는 경우

import sys

num = int(sys.stdin.readline())


### 한 줄에 값 여러개 입력 받기

import sys

a, b, c = map(int,sys.stdin.readline().split())
num_list = list(map(int,sys.stdin.readline().split()))


### 문자열 여러 줄 입력 받기

# 문자열을 n 줄 입력받는다고 가정.
import sys

data = []
n = int(sys.stdin.readline())
for i in range(n):
    data.append(list(map(int,sys.stdin.readline().split())))
