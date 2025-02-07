
num_list = []
maxValue = -1
max_index = 0
for i in range(9):
    num_list.append(int(input()))

num_list_count = len(num_list)    
for i in range(num_list_count):
    if num_list[i] > maxValue:
        maxValue = num_list[i]    
        max_index = i
print(maxValue)
print(max_index + 1)
        
        
# lst = []
# for _ in range(9) :
#     lst.append(int(input()))

# print(max(lst))
# print(lst.index(max(lst))+1)