class Search:
    
    def linear_search(list, target):
        for i in range(0, len(list)):
            if(list[i] == target):
                return i
            
        return -1
    
    def binary_search(list, target):
        first = 0
        last = len(list) -1
        
        while first <= last:
            mid = (first + last ) // 2
            if(list[mid] == target):
                return mid
            elif list[mid] < target:
                first = mid + 1
            else:
                last = mid -1
                
        return list[first] if abs(target - list[first]) < abs(target - list[last]) else list[last]
    
    def hash_search(list, target):
        hash_table = {}
        for i in range(0, len(list)):
            hash_table[list[i]] = i
        if target in hash_table:
            return hash_table[target]
        return -1
    
    def brute_force_search(list, target):
        for i in range(0, len(list)):
            for j in range(i + 1, len(list)):
                if list[i] + list[j] == target:
                    return [i, j]
        return None

    def recursive_binary_search(list, target):
        if len(list) == 0:
            return False
        else:
            midpoint = len(list) // 2
            if list[midpoint] == target:
                return True 
            else:
                if list[midpoint] < target:
                    return Search.recursive_binary_search(list[midpoint + 1:], target)
                else:
                    return  Search.recursive_binary_search(list[:midpoint], target)


print( Search.linear_search([1,2,3,4,5], 5)) 

print( Search.binary_search([1,2,3,4,8], 5))

print( Search.hash_search([1,2,3,4,5], 5))

print( Search.brute_force_search([1,2,3,4,5], 5))

print( Search.recursive_binary_search([1,2,3,4,5], 5))