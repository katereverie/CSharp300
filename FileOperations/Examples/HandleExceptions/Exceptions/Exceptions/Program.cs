
int[] arr = [1, 2, 3];

arr[3] = 4;

throw new Exception("Oops.");

// other derived types
throw new ArgumentException("parameters cannot be string", "name");