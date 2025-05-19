import heapq

# 힙 생성 및 기본 연산
heap = []
heapq.heappush(heap, 3)  # 힙에 3 추가
heapq.heappush(heap, 1)  # 힙에 1 추가
heapq.heappush(heap, 2)  # 힙에 2 추가
print(heap)  # [1, 3, 2] - 힙 내부 구조는 완전 이진 트리

smallest = heapq.heappop(heap)  # 가장 작은 요소 제거 및 반환
print(smallest, heap)  # 1 [2, 3] - 최소값이 제거됨

# 기존 리스트를 힙으로 변환
numbers = [3, 1, 4, 1, 5, 9]
heapq.heapify(numbers)  # 리스트를 힙으로 변환 (O(n) 시간)
print(numbers)  # [1, 1, 4, 3, 5, 9] - 힙 구조로 재배열됨

# 사용 예제: K번째 작은 요소 찾기
def kth_smallest(nums, k):
    heap = nums[:k]  # 처음 k개 요소로 힙 생성
    heapq.heapify(heap)
    
    for num in nums[k:]:
        if num < heap[0]:  # 현재 최소값보다 작은 경우
            continue
        heapq.heappushpop(heap, num)  # 현재 최소값 제거 후 새 값 추가
    
    return heap[0]  # k번째 작은 요소는 힙의 루트

print(kth_smallest([3, 1, 4, 1, 5, 9, 2, 6], 3))  # 2 - 3번째 작은 요소