#include <cstdio>
#include <iostream>

using namespace std;

typedef unsigned long long uint64;
typedef unsigned long uint32;

#define N 8 
#define F32 0xFFFFFFFF 
#define size64 sizeof(uint64) 

#define ROR(x, n, xsize) ((x >> n) | (x << (xsize - n)))
#define ROL(x, n, xsize) ((x << n) | (x >> (xsize - n)))

#define RKEY(r) ((ROR(K, r * 3, size64 * 8)) & F32)

const uint64 K = 0x96E243A67D7BCFB1;
uint32 RK[N];

void createRKeys(bool print) {
    for (int i = 0; i < N; i++) {
        RK[i] = (ROR(K, i * 8, size64 * 8)) & F32;
        if (print) {
            cout << "key[" << i << "] = " << hex << RK[i] << endl;
        }
    }
}

uint32 F(uint32 subblk, uint32 key) {
    uint32 f1 = ROL(subblk, 9, sizeof(subblk) * 8);
    uint32 f2 = ROR(key, 11, sizeof(key) * 8) | subblk;
    return f1 ^ !f2;
}


uint64 encrypt(uint64 block, bool print) {
    uint32 left_, right_;
    uint32 left = (block >> 32) & F32;
    uint32 right = block & F32;

    for (int r = 0; r < N; r++) {
        if (print) {
            cout << "round " << r << endl;
            printf("input blk = %x %x\n", left, right);
        }

        uint32 fk = F(left, RK[r]);
        left_ = left;
        right_ = right ^ fk;
        if (r < N - 1) {
            left = right_;
            right = left_;
        }
        else {
            right = right_;
            left = left_;
        }

        if (print) {
            printf("output blk = %x %x\n", left, right);
        }
    }

    uint64 c_block = left;
    c_block = (c_block << 32) | (right & F32);
    return c_block;
}

uint64 decrypt(uint64 block, bool print) {
    uint32 left_, right_;
    uint32 left = (block >> 32) & F32;
    uint32 right = block & F32;

    for (int r = N - 1; r >= 0; r--) {
        if (print) {
            cout << "round " << r << endl;
            printf("input blk = %x %x\n", left, right);
        }

        uint32 fk = F(left, RK[r]);
        left_ = left;
        right_ = right ^ fk;
        if (r > 0) {
            left = right_;
            right = left_;
        }
        else {
            right = right_;
            left = left_;
        }

        if (print) {
            printf("output blk = %x %x\n", left, right);
        }
    }

    uint64 c_block = left;
    c_block = (c_block << 32) | (right & F32);
    return c_block;
}

int main() {
    cout << "key " << hex << K << endl;
    createRKeys(false);

    uint64 msg = 0xF1D4A2C7960BCDF2;
    cout << "message\n" << hex << msg << endl;

    uint64 c_msg = encrypt(msg, false);
    cout << "enc message\n" << hex << c_msg << endl;

    uint64 msg_ = decrypt(c_msg, false);
    cout << "dec message\n" << hex << msg_ << endl;
}
