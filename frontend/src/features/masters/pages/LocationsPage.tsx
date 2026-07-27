import { useState } from 'react';

import StateSection from '../components/locations/StateSection';
import DivisionSection from '../components/locations/DivisionSection';
import DistrictSection from '../components/locations/DistrictSection';
import BlockSection from '../components/locations/BlockSection';
import GramPanchayatSection from '../components/locations/GramPanchayatSection';

type SubTab = 'states' | 'divisions' | 'districts' | 'blocks' | 'gramPanchayats';

const subTabs: { key: SubTab; label: string }[] = [
  { key: 'states', label: 'States' },
  { key: 'divisions', label: 'Divisions' },
  { key: 'districts', label: 'Districts' },
  { key: 'blocks', label: 'Blocks' },
  { key: 'gramPanchayats', label: 'Gram Panchayats' },
];

export default function LocationsPage() {
  const [activeTab, setActiveTab] = useState<SubTab>('states');

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Location Masters</h1>
          <p>Manage the administrative hierarchy: State → Division → District → Block → Gram Panchayat</p>
        </div>
      </div>

      <div style={{ display: 'flex', gap: 4, marginBottom: 24, borderBottom: '1px solid var(--border-color)', paddingBottom: 0 }}>
        {subTabs.map((t) => (
          <button
            key={t.key}
            onClick={() => setActiveTab(t.key)}
            style={{
              padding: '10px 16px',
              fontSize: 13,
              fontWeight: 600,
              border: 'none',
              background: 'transparent',
              color: activeTab === t.key ? 'var(--accent-primary)' : 'var(--text-secondary)',
              borderBottom: activeTab === t.key ? '2px solid var(--accent-primary)' : '2px solid transparent',
              cursor: 'pointer',
              transition: 'all 0.15s',
            }}
          >
            {t.label}
          </button>
        ))}
      </div>

      {activeTab === 'states' && <StateSection />}
      {activeTab === 'divisions' && <DivisionSection />}
      {activeTab === 'districts' && <DistrictSection />}
      {activeTab === 'blocks' && <BlockSection />}
      {activeTab === 'gramPanchayats' && <GramPanchayatSection />}
    </div>
  );
}
