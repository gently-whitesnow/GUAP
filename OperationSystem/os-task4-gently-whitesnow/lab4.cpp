#include "lab4.h"
#include <iostream>
#include <queue>
#include <vector>

int NUM_PAGES = 6;
int INTERAPTION_CALLS = 5;

struct Page {
  int vpn;     // Виртуальный номер страницы
  bool r;      // Счетчик обращений
  int counter; // Счетчик для NFU
  Page(int vpn) : vpn(vpn), r(true), counter(0) {}
};

void printTable(const std::vector<Page *> &table) {
  for (size_t i = 0; i < table.size(); ++i) {
    if (table[i]) {
      std::cout << table[i]->vpn;
    } else {
      std::cout << "#";
    }
    if (i < table.size() - 1) {
      std::cout << " "; // Пробел только между элементами
    }
  }
  std::cout << std::endl;
}

void cleanupTable(std::vector<Page *> &table) {
  for (auto &page : table) {
    delete page;
    page = nullptr;
  }
}

bool tryReadFifoPage(const std::vector<Page *> &table, int vpn) {
  for (auto &page : table) {
    if (page && page->vpn == vpn) {
      return true;
    }
  }
  return false;
}

void replaceOrAddFifoPage(std::vector<Page *> &table,
                          std::queue<int> &fifoQueue, int vpn) {
  if (fifoQueue.size() == NUM_PAGES) {
    int toReplace = fifoQueue.front();
    fifoQueue.pop();

    for (auto &page : table) {
      if (page && page->vpn == toReplace) {
        page->vpn = vpn;
        fifoQueue.push(vpn);
        return;
      }
    }
  } else {
    for (auto &page : table) {
      if (!page) {
        page = new Page(vpn);
        fifoQueue.push(vpn);
        return;
      }
    }
  }
}

void fifoAlgorithm(const std::vector<std::pair<int, int>> &operations) {
  std::vector<Page *> table(NUM_PAGES, nullptr);
  std::queue<int> fifoQueue;
  int pageFaults = 0;

  for (const auto &op : operations) {
    int vpn = op.second;

    if (!tryReadFifoPage(table, vpn)) {
      ++pageFaults;
      replaceOrAddFifoPage(table, fifoQueue, vpn);
    }

    printTable(table);
  }

  cleanupTable(table);
  // std::cout << "FIFO Page Faults: " << pageFaults << std::endl;
}

void interaption(std::vector<Page *> &table) {
  for (auto &page : table) {
    if (page && page->r) {
      page->counter++;
      page->r = false;
    }
  }
}

bool tryReadNfuPage(const std::vector<Page *> &table, int vpn) {
  for (auto &page : table) {
    if (page && page->vpn == vpn) {
      page->r = true;
      return true;
    }
  }
  return false;
}

void handleInteraptionIfNeeded(int accessCounter, std::vector<Page *> &table) {
  if (accessCounter % INTERAPTION_CALLS == 0) {
    interaption(table);
  }
}

Page *selectPageToReplace(const std::vector<Page *> &table, int vpn) {
  unsigned int min = UINT32_MAX;
  for (auto &page : table) {
    if (page && page->counter < min) {
      min = page->counter;
    }
  }

  std::vector<Page *> candidates;
  for (auto &page : table) {
    if (page && page->counter == min) {
      candidates.push_back(page);
    }
  }

  if (candidates.size() == 1) {
    return candidates[0];
  } else {
    int rndPos = uniform_rnd(0, (int)candidates.size() - 1);
    return candidates[rndPos];
  }
}

void replaceOrAddNfuPage(std::vector<Page *> &table, int vpn) {
  bool allPagesExist = true;
  for (Page *p : table) {
    if (!p) {
      allPagesExist = false;
      break;
    }
  }
  if (allPagesExist) {
    Page *toReplace = selectPageToReplace(table, vpn);
    toReplace->vpn = vpn;
    toReplace->r = true;
    toReplace->counter = 0;
  } else {
    for (auto &page : table) {
      if (!page) {
        page = new Page(vpn);
        break;
      }
    }
  }
}

void nfuAlgorithm(const std::vector<std::pair<int, int>> &operations) {
  std::vector<Page *> table(NUM_PAGES, nullptr);
  int pageFaults = 0;
  int accessCounter = 0;

  for (const auto &op : operations) {
    int vpn = op.second;

    accessCounter++;

    if (!tryReadNfuPage(table, vpn)) {
      pageFaults++;
      replaceOrAddNfuPage(table, vpn);
    }
    handleInteraptionIfNeeded(accessCounter, table);
    printTable(table);
  }

  cleanupTable(table);
  // std::cout << "NFU Page Faults: " << pageFaults << std::endl;
}

int main(int argc, char *argv[]) {
  if (argc < 2) {
    std::cerr << "Usage: " << argv[0] << " <algorithm_number>\n";
    return 1;
  }

  int algorithm = std::stoi(argv[1]);

  std::vector<std::pair<int, int>> operations;
  int type, vpn;
  while (std::cin >> type >> vpn) {
    operations.emplace_back(type, vpn);
  }

  // в зависимости от алгоритма вызываем соответствующую функцию
  if (algorithm == 1) {
    fifoAlgorithm(operations);
  } else if (algorithm == 2) {
    nfuAlgorithm(operations);
  } else {
    std::cerr << "Invalid algorithm number.\n";
    return 1;
  }

  return 0;
}
