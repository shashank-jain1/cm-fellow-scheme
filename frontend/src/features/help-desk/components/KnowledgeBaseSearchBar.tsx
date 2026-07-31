import React from 'react';
import { AppInput } from '../../../shared/components/forms';

interface KnowledgeBaseSearchBarProps {
  searchQuery: string;
  setSearchQuery: (q: string) => void;
}

export default function KnowledgeBaseSearchBar({ searchQuery, setSearchQuery }: KnowledgeBaseSearchBarProps) {
  return (
    <div style={{ display: 'flex', gap: 12, marginBottom: 'var(--space-4)', maxWidth: 480 }}>
      <div style={{ flex: 1 }}>
        <AppInput
          value={searchQuery}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearchQuery(e.target.value)}
          placeholder="Search by keyword or category..."
        />
      </div>
    </div>
  );
}
