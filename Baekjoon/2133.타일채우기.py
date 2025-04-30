def count_tile_ways(N):    
    if N < 2:
        return 0
    elif N == 2:
        return 3
    
    dp = [0] * (N + 1)
    
    dp[0] = 1
    dp[2] = 3
    
    for i in range(4, N + 1, 2):
        dp[i] = 4* dp[i-2] - dp[i-4]
        
    return dp[N]


import sys
input = sys.stdin.read

N = int(input().strip())

print(count_tile_ways(N))