import random
import time

def selectionSort(arr):
    n = len(arr)
    for i in range(n-1):
        for j in range(i + 1, n):
            if(arr[i] > arr[j]):
                arr[i], arr[j] = arr[j], arr[i]
                
        

class Sort:
    def bubble_sort(arr):
        for i in range(len(arr)):
            for j in range(len(arr) - 1):
                if arr[j] > arr[j+1]:
                    arr[j], arr[j+1] = arr[j+1], arr[j]
        return arr
    
    def selection_sort(arr):
        for i in range(len(arr)):
            min_index = i
            for j in range(i+1, len(arr)):
                if(arr[min_index] > arr[j]):
                    min_index = j
                arr[i], arr[min_index] = arr[min_index], arr[i]
        return arr
    
    def quick_sort(arr):
        if(len(arr) <= 1):
            return arr
        pivot = arr[len(arr) // 2]
        left, right, equal =[], [], []
        for i in arr:
            if i < pivot:      
                left.append(i)
            elif i > pivot:
                right.append(i)
            else:
                equal.append(i)
                
        return Sort.quick_sort(left) + equal + Sort.quick_sort(right)
    
    def heap_sort(arr):
        def heapify(arr, n, i):
            largest = i
            l = 2 * i + 1
            r = 2 * i + 2

            if l < n and arr[i] < arr[l]:
                largest = l

            if r < n and arr[largest] < arr[r]:
                largest = r

            if largest != i:
                arr[i], arr[largest] = arr[largest], arr[i]
                heapify(arr, n, largest)

        n = len(arr)
        for i in range(n, -1, -1):
            heapify(arr, n, i)

        for i in range(n-1, 0, -1):
            arr[i], arr[0] = arr[0], arr[i]
            heapify(arr, i, 0)

        return arr
            
            
    def merge_sort(arr):
        if(len(arr) < 2 ):
            return arr
        
        mid = len(arr) // 2
        left = Sort.merge_sort(arr[:mid])
        right = Sort.merge_sort(arr[mid:])
        
        merge_arr = []
        l = r = 0
        while l < len(left) and r < len(right):
            if left[l] < right[r]:
                merge_arr.append(left[l])
                l += 1
            else:
                merge_arr.append(right[r])
                r += 1
        merge_arr += left[l:]
        merge_arr += right[r:]
        return merge_arr
    
    def radix_sort(arr):
        RADIX = 10
        placement = 1

        max_digit = max(arr)

        while placement < max_digit:
            buckets = [list() for _ in range(RADIX)]

            for i in arr:
                tmp = int((i / placement) % RADIX)
                buckets[tmp].append(i)

            a = 0
            for b in range(RADIX):
                buck = buckets[b]
                for i in buck:
                    arr[a] = i
                    a += 1

            placement *= RADIX

        return arr


    def counting_sort(arr):
        max_value = max(arr)
        m = max_value + 1
        count = [0] * m

        for a in arr:
            count[a] += 1

        i = 0
        for a in range(m):
            for c in range(count[a]):
                arr[i] = a
                i += 1
        return arr 
# data = [2, 5,3 ,1 ,4]
# selectionSort(data)
# print(data)
arr = [i for i in random.sample(range(10), 10)]





print("Original array: ", arr)
start_time = time.time()  # 시작 시간 기록
print("Bubble sort: ", Sort.bubble_sort(arr))
end_time = time.time()  # 종료 시간 기록
execution_time = end_time - start_time
print(f"함수 실행 시간: {execution_time:.4f}초")

start_time = time.time()  # 시작 시간 기록
print("Selection sort: ", Sort.selection_sort(arr))
end_time = time.time()  # 종료 시간 기록
execution_time = end_time - start_time
print(f"함수 실행 시간: {execution_time:.4f}초")

start_time = time.time()  # 시작 시간 기록
print("Quick sort: ", Sort.quick_sort(arr))
end_time = time.time()  # 종료 시간 기록
execution_time = end_time - start_time
print(f"함수 실행 시간: {execution_time:.4f}초")

start_time = time.time()  # 시작 시간 기록
print("Merge sort: ", Sort.merge_sort(arr))
end_time = time.time()  # 종료 시간 기록
execution_time = end_time - start_time
print(f"함수 실행 시간: {execution_time:.4f}초")

start_time = time.time()  # 시작 시간 기록
print("Heap sort: ", Sort.heap_sort(arr))
end_time = time.time()  # 종료 시간 기록
execution_time = end_time - start_time
print(f"함수 실행 시간: {execution_time:.4f}초")

start_time = time.time()  # 시작 시간 기록
print("Radix sort: ", Sort.radix_sort(arr))
end_time = time.time()  # 종료 시간 기록
execution_time = end_time - start_time
print(f"함수 실행 시간: {execution_time:.4f}초")

start_time = time.time()  # 시작 시간 기록
print("Counting sort: ", Sort.counting_sort(arr))
end_time = time.time()  # 종료 시간 기록
execution_time = end_time - start_time
print(f"함수 실행 시간: {execution_time:.4f}초")
