import { useState } from 'react';
import { AppInput, AppTextarea } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { useCreateKBArticle } from '../queries';
import type { CreateKBArticleCommand } from '../types';

interface Props {
  onCreated?: () => void;
}

export default function KnowledgeBaseArticleForm({ onCreated }: Props) {
  const [title, setTitle] = useState('');
  const [category, setCategory] = useState('');
  const [content, setContent] = useState('');
  const createMutation = useCreateKBArticle();

  const handleSubmit = async () => {
    if (!title.trim() || !content.trim()) return;
    const command: CreateKBArticleCommand = { title: title.trim(), category: category.trim(), content: content.trim() };
    await createMutation.mutateAsync(command);
    setTitle('');
    setCategory('');
    setContent('');
    onCreated?.();
  };

  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ marginBottom: 16 }}>New Article</h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
        <AppInput value={title} onChange={(e: React.ChangeEvent<HTMLInputElement>) => setTitle(e.target.value)} placeholder="Title" />
        <AppInput value={category} onChange={(e: React.ChangeEvent<HTMLInputElement>) => setCategory(e.target.value)} placeholder="Category" />
        <AppTextarea value={content} onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setContent(e.target.value)} placeholder="Content" rows={6} />
        <div>
          <AppButton variant="primary" onClick={handleSubmit} loading={createMutation.isPending} disabled={!title.trim() || !content.trim()}>
            Publish
          </AppButton>
        </div>
      </div>
    </div>
  );
}
