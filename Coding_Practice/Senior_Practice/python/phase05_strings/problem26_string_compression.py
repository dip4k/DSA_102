def compress(chars: list[str]) -> int:
    write = 0
    read = 0
    n = len(chars)

    while read < n:
        curr = chars[read]
        run_start = read
        while read < n and chars[read] == curr:
            read += 1

        count = read - run_start
        chars[write] = curr
        write += 1

        if count > 1:
            for digit in str(count):
                chars[write] = digit
                write += 1

    return write
