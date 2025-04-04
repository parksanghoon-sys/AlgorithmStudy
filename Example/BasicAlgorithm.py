
"""
파이썬 기초 알고리즘 정리 - 코딩테스트 준비
============================================
"""

# 1. 배열과 리스트 다루기
"""
파이썬의 리스트는 코딩테스트에서 가장 기본적으로 사용되는 자료구조입니다.
"""

# 예제 1: 리스트 조작 기본

def list_operations():
    # 1. 리스트와 배열 기본 연산
    print("1. 리스트와 배열 기본 연산")
    # 리스트 초기화
    arr = [1, 2, 3, 4, 5]
    print(f"초기 리스트: {arr}")

    # 요소 추가
    arr.append(6)  # 끝에 추가
    print(f"append(6) 후: {arr}")

    # 특정 위치에 요소 삽입
    arr.insert(1, 10)  # 인덱스 1에 10 삽입
    print(f"insert(1, 10) 후: {arr}")

    # 요소 제거
    arr.remove(10)  # 값이 10인 첫 번째 요소 제거
    print(f"remove(10) 후: {arr}")

    # 특정 인덱스 요소 꺼내기
    popped = arr.pop(1)  # 인덱스 1의 요소 꺼내기
    print(f"pop(1) 후: {arr}, 꺼낸 값: {popped}")

    # 슬라이싱
    print(f"arr[1:4]: {arr[1:4]}")  # 인덱스 1부터 3까지
    print(f"arr[:3]: {arr[:3]}")    # 처음부터 인덱스 2까지
    print(f"arr[2:]: {arr[2:]}")    # 인덱스 2부터 끝까지

    # 리스트 정렬
    arr.sort()  # 오름차순 정렬
    print(f"오름차순 정렬: {arr}")
    arr.sort(reverse=True)  # 내림차순 정렬
    print(f"내림차순 정렬: {arr}")
    
    # 리스트 뒤집기
    arr.reverse()
    print(f"뒤집기 후: {arr}")

    print("\n" + "-"*50 + "\n")
    
    
    
# 2. 스택과 큐
def stack_operations():
    print("2. 스택과 큐")

    # 스택(Stack) - LIFO(Last-In-First-Out)
    print("스택(Stack) 구현:")
    stack = []
    stack.append(1)
    stack.append(2)
    stack.append(3)
    print(f"스택에 1, 2, 3 추가 후: {stack}")

    print(f"pop(): {stack.pop()}")  # 마지막에 넣은 3이 출력됨
    print(f"pop() 후 스택: {stack}")
    print(f"pop(): {stack.pop()}")  # 그 다음 2가 출력됨
    print(f"pop() 후 스택: {stack}")


        
    # 큐(Queue) - FIFO(First-In-First-Out)
    print("\n큐(Queue) 구현:")
    from collections import deque
    queue = deque()
    queue.append(1)
    queue.append(2)
    queue.append(3)
    print(f"큐에 1, 2, 3 추가 후: {queue}")

    print(f"popleft(): {queue.popleft()}")  # 가장 먼저 넣은 1이 출력됨
    print(f"popleft() 후 큐: {queue}")
    print(f"popleft(): {queue.popleft()}")  # 그 다음 2가 출력됨
    print(f"popleft() 후 큐: {queue}")

print("\n" + "-"*50 + "\n")

# 3. 정렬 알고리즘
def sort_operations():
    # 파이썬 내장 정렬: sort()와 sorted()
    # sort()는 리스트를 직접 정렬, sorted()는 정렬된 새 리스트 반환
    numbers = [64, 34, 25, 12, 22, 11, 90]
    print(f"원본 리스트: {numbers}")

    # sorted() 함수 (원본 유지, 새로운 정렬된 리스트 반환)
    sorted_numbers = sorted(numbers)
    print(f"sorted() 결과: {sorted_numbers}")
    print(f"원본 리스트 (변경 없음): {numbers}")

    # sort() 메소드 (원본 리스트 직접 정렬)
    numbers.sort()
    print(f"sort() 후 리스트: {numbers}")

    # 내림차순 정렬
    numbers.sort(reverse=True)
    print(f"내림차순 정렬: {numbers}")

    # key 함수를 사용한 정렬
    students = [
        {"name": "김철수", "score": 85},
        {"name": "이영희", "score": 92},
        {"name": "박민수", "score": 78}
    ]
    # 점수를 기준으로 정렬
    sorted_students = sorted(students, key=lambda x:x['score'],reverse=True)
    print(f"점수 오름차순 정렬:")
    for student in sorted_students:
        print(f"  {student['name']}: {student['score']}점")

    print("\n" + "-"*50 + "\n")
    
    # 4. 탐색 알고리즘
class serch_operations:
    # # 탐색 알고리즘 테스트
    # arr = [1, 3, 5, 7, 9, 11, 13, 15, 17, 19]
    # target = 13

    # search = serch_operations(arr,target=target)

    # print(f"배열: {arr}")
    # print(f"찾는 값: {target}")

    # # 선형 탐색 테스트
    # linear_result = search.linear_search()
    # print(f"선형 탐색 결과: 인덱스 {linear_result}")

    # # 이진 탐색 테스트
    # binary_result = search.binary_search()
    # print(f"이진 탐색 결과: 인덱스 {binary_result}")
    
    def __init__(self, arr, target):
        print("4. 탐색 알고리즘")
        self.arr = arr
        self.target = target
        print("\n" + "-"*50 + "\n")
        pass
    
    # 선형 탐색(Linear Search)
    def linear_search(self):
        for i in range(len(self.arr)):
            if self.arr[i] == self.target:
                return i
        return -1
    
    # 이진 탐색
    def binary_search(self):        
        left, right = 0, len(self.arr) -1
        
        while left <= right:
            mid = (left + right) //2
            
            if(self.arr[mid] == self.target):
                return mid
            elif self.arr[mid] < self.target:
                left = mid + 1
            else:
                right = mid -1
        return -1


# 그래프 표현 - 이접 리스트 방식

    
def dfs(graph, start, visited=None):
    """깊이 우선 탐색 알고리즘"""
    if visited is None:
        visited = set()
    
    visited.add(start)
    print(start, end=' ')
    
    for next in graph[start]:
        if next not in visited:
            dfs(graph, next, visited)
    
    return visited   

         
def bfs(graph, start):
    from collections import deque
    """너비 우선 탐색 알고리즘"""
    visited = set([start])
    
    queue = deque([start])
    result = []
    
    while queue:
        vertex = queue.popleft()
        result.append(vertex)
        
        for neighbor in graph[vertex]:
            if neighbor not in visited:
                visited.add(neighbor)            
                queue.append(neighbor)
                
    return result

# graph = {
#     'A': ['B', 'C'],
#     'B': ['A', 'D', 'E'],
#     'C': ['A', 'F'],
#     'D': ['B'],
#     'E': ['B', 'F'],
#     'F': ['C', 'E']
#     }
# print("\nDFS 탐색 결과:", end=' ')
# dfs(graph, 'A')

# print("\nBFS 탐색 결과:", end=' ')
# bfs_result = bfs(graph, 'A')
# print(' '.join(bfs_result))
print("\n" + "-"*50 + "\n")

# 6. 동적 프로그래밍 (DP)
print("6. 동적 프로그래밍 (DP)")

# 피보나치 수열 - 일반 재귀 구현
def fib_recursive(n):
    """피보나치 수열 - 재귀 구현"""
    if n <= 1:
        return n
    return fib_recursive(n-1) + fib_recursive(n-2)

# 피보나치 수열 - DP (메모이제이션) 구현
def fib_dp(n, memo={}):
    """피보나치 수열 - DP 구현 (메모이제이션)"""
    if n in memo:
        return memo[n]
    if n <= 1:
        return n
    
    memo[n] = fib_dp(n-1, memo) + fib_dp(n-2, memo)
    return memo[n]

# 피보나치 수열 - 반복문 구현
def fib_iterative(n):
    """피보나치 수열 - 반복문 구현"""
    if n <= 1:
        return n
    
    a, b = 0, 1
    for _ in range(2, n+1):
        a, b = b, a + b
    return b

# # 피보나치 수열 테스트 (작은 수로 테스트)
# n = 10
# print(f"피보나치 수열의 {n}번째 수:")
# print(f"  재귀 구현: {fib_recursive(n)}")
# print(f"  DP 구현: {fib_dp(n)}")
# print(f"  반복문 구현: {fib_iterative(n)}")

print("\n" + "-"*50 + "\n")

# # 7. 그리디 알고리즘
# print("7. 그리디 알고리즘")

# 거스름돈 문제
def coin_change_greedy(amount, coins):
    """그리디 알고리즘을 이용한 거스름돈 문제"""
    coins.sort(reverse=True)  # 동전을 큰 단위부터 정렬
    count = 0
    coin_used = {}
    
    for coin in coins:
        # 현재 동전으로 거슬러 줄 수 있는 개수 계산
        coin_count = amount // coin
        if coin_count > 0:
            coin_used[coin] = coin_count
            count += coin_count
            amount -= coin * coin_count
    
    # 거슬러 줄 수 없으면 -1 반환
    if amount > 0:
        return -1, {}
    
    return count, coin_used

# # 거스름돈 문제 테스트
# amount = 1260
# coins = [500, 100, 50, 10]
# total_coins, coins_used = coin_change_greedy(amount, coins)

# print(f"거스름돈 {amount}원을 동전 {coins}로 거슬러 주기:")
# print(f"  필요한 동전 개수: {total_coins}개")
# print("  사용한 동전:")
# for coin, count in coins_used.items():
#     print(f"    {coin}원: {count}개")

print("\n" + "-"*50 + "\n")

# 8. 해시 테이블 (딕셔너리)
# print("8. 해시 테이블 (딕셔너리)")

# # 딕셔너리 기본 사용법
# hash_table = {}
# hash_table['apple'] = 5
# hash_table['banana'] = 3
# hash_table['cherry'] = 7

# print(f"해시 테이블: {hash_table}")
# print(f"'apple'의 값: {hash_table['apple']}")

# # 키 존재 여부 확인
# print(f"'grape'가 존재하는가? {'grape' in hash_table}")
# print(f"'apple'가 존재하는가? {'apple' in hash_table}")

# # get 메소드 - 키가 없을 때 기본값 반환
# print(f"get('grape', 0): {hash_table.get('grape', 0)}")

# 빈도수 세기 문제
def count_frequency(arr):
    """배열 요소의 빈도수 세기"""
    frequency = {}
    for item in arr:
        if item in frequency:
            frequency[item] += 1
        else:
            frequency[item] = 1
    return frequency

# # 빈도수 세기 테스트
# fruits = ['apple', 'banana', 'apple', 'cherry', 'apple', 'banana']
# fruit_counts = count_frequency(fruits)

# print("\n과일 빈도수:")
# for fruit, count in fruit_counts.items():
#     print(f"  {fruit}: {count}개")

print("\n" + "-"*50 + "\n")


# 9. 힙 (우선순위 큐)
# print("9. 힙 (우선순위 큐)")

# import heapq

# min_heap = []
# heapq.heappush(min_heap, 5)
# heapq.heappush(min_heap, 3)
# heapq.heappush(min_heap, 7)
# heapq.heappush(min_heap, 1)


# print(f"최소 힙: {min_heap}")  # 내부 표현은 완전 이진 트리를 배열로 표현
# print(f"힙에서 최솟값 꺼내기: {heapq.heappop(min_heap)}")
# print(f"꺼낸 후 힙: {min_heap}")

# # 최대 힙 구현 (값에 -1을 곱하여 구현)
# max_heap = []
# heapq.heappush(max_heap, -5)
# heapq.heappush(max_heap, -3)
# heapq.heappush(max_heap, -7)
# heapq.heappush(max_heap, -1)

# print(f"\n최대 힙 (내부 표현): {max_heap}")
# max_value = -heapq.heappop(max_heap)  # 최댓값을 꺼내고 부호 변환
# print(f"힙에서 최댓값 꺼내기: {max_value}")
# print(f"꺼낸 후 힙 (내부 표현): {max_heap}")

# 우선순위 큐 활용 - K번째 큰 요소 찾기
def find_kth_largest(nums, k):
    import heapq
    """배열에서 K번째 큰 요소 찾기"""
    # 최소 힙을 사용해 k개의 가장 큰 요소 유지
    min_heap = []
    for num in nums:
        # 힙에 요소 추가
        heapq.heappush(min_heap, num)
        # 힙 크기가 k를 초과하면 가장 작은 요소 제거
        if len(min_heap) > k:
            heapq.heappop(min_heap)
    
    # k개의 가장 큰 요소 중 가장 작은 요소 반환
    return min_heap[0]

# # K번째 큰 요소 찾기 테스트
# nums = [3, 2, 1, 5, 6, 4]
# k = 2
# print(f"\n배열 {nums}에서 {k}번째 큰 요소: {find_kth_largest(nums, k)}")

print("\n" + "-"*50 + "\n")

# 1. 두 수의 합
print("문제 1: 두 수의 합")
"""
문제: 정수 배열 nums와 정수 target이 주어지면, 두 수의 합이 target이 되는 인덱스를 반환하세요.
각 입력에 정확히 하나의 해가 있다고 가정하며, 같은 요소를 두 번 사용할 수 없습니다.
"""

def two_sum(nums, target):
    # 해시맵을 사용하여 O(n) 시간복잡도로 해결
    num_map = {}  # 값: 인덱스
    
    for i, num in enumerate(nums):
        complement = target - num  # 필요한 보수 계산
        
        if complement in num_map:  # 보수가 이미 해시맵에 있는지 확인
            return [num_map[complement], i]
        
        num_map[num] = i  # 현재 숫자와 인덱스를 해시맵에 저장
    
    return []  # 해답이 없는 경우 (문제 조건에 따라 발생하지 않음)


# 테스트
nums = [2, 7, 11, 15]
target = 9
print(f"입력: nums = {nums}, target = {target}")
print(f"출력: {two_sum(nums, target)}")  # [0, 1] 반환 예상

# 2. 올바른 괄호 검사
print("\n문제 2: 올바른 괄호 검사")
"""
문제: 괄호 문자열 s가 주어지면, 괄호가 올바르게 짝지어졌는지 확인하세요.
'(', ')', '{', '}', '[', ']' 이 포함됩니다.
"""


def is_valid_parentheses(s):
    # 스택을 사용하여 괄호 짝 검사
    stack = []
    # 괄호 쌍 매핑
    brackets = {')': '(', '}': '{', ']': '['}
    
    for char in s:
        # 여는 괄호면 스택에 추가
        if char in '({[':
            stack.append(char)
        # 닫는 괄호면 스택에서 꺼내 짝이 맞는지 확인
        elif char in ')}]':
            # 스택이 비어있거나 짝이 맞지 않으면 False
            if not stack or stack.pop() != brackets[char]:
                return False
    
    # 모든 처리 후 스택이 비어있어야 모든 괄호의 짝이 맞음
    return len(stack) == 0

# 테스트
parentheses = "()[]{}"
print(f"입력: s = \"{parentheses}\"")
print(f"출력: {is_valid_parentheses(parentheses)}")  # True 반환 예상

parentheses = "([)]"
print(f"입력: s = \"{parentheses}\"")
print(f"출력: {is_valid_parentheses(parentheses)}")  # False 반환 예상

# 3. 최대 부분 배열 합
print("\n문제 3: 최대 부분 배열 합")
"""
문제: 정수 배열 nums가 주어지면, 연속된 부분 배열의 최대 합을 찾으세요.
"""

def max_subarray(nums):
    # 카데인 알고리즘(Kadane's algorithm) 사용
    current_sum = max_sum = nums[0]
    
    for num in nums[1:]:
        # 현재까지의 합과 현재 원소를 더한 값, 또는 현재 원소 중 큰 값 선택
        current_sum = max(num, current_sum + num)
        # 최대 합 갱신
        max_sum = max(max_sum, current_sum)
    
    return max_sum

# 테스트
nums = [-2, 1, -3, 4, -1, 2, 1, -5, 4]
print(f"입력: nums = {nums}")
print(f"출력: {max_subarray(nums)}")  # 6 반환 예상 (부분 배열 [4,-1,2,1])