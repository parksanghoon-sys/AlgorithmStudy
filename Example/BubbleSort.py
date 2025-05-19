def bubble_sort(arr):
    """
    버블 정렬: 인접한 두 원소를 비교하여 정렬하는 방식
    시간 복잡도: O(n^2)
    공간 복잡도: O(1)
    """
    
    n = len(arr)
    
    for i in range(n):
        for j in range(0, n - i -1):
            if(arr[j] > arr[j+1]):
                arr[j], arr[j+1] = arr[j+1], arr[j]
    return arr

input_data = [5,3,8,4,2]
result = bubble_sort(input_data)

print(result)