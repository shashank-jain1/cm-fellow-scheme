import { AppButton } from '../../../shared/components/ui';
import { useExportDashboardPdf, useExportDashboardExcel } from '../queries';
import type { DashboardFilters } from '../types';

interface DashboardExportProps {
  filters?: DashboardFilters;
}

export default function DashboardExport({ filters }: DashboardExportProps) {
  const exportPdf = useExportDashboardPdf();
  const exportExcel = useExportDashboardExcel();

  const handleExport = async (format: 'pdf' | 'excel') => {
    const mutation = format === 'pdf' ? exportPdf : exportExcel;
    const result = await mutation.mutateAsync(filters);
    if (result?.fileUrl) {
      window.open(result.fileUrl, '_blank');
    }
  };

  return (
    <div style={{ display: 'flex', gap: 8 }}>
      <AppButton
        variant="secondary"
        size="sm"
        icon="pi pi-file-pdf"
        onClick={() => handleExport('pdf')}
        loading={exportPdf.isPending}
        disabled={exportPdf.isPending || exportExcel.isPending}
      >
        Export PDF
      </AppButton>
      <AppButton
        variant="secondary"
        size="sm"
        icon="pi pi-file-excel"
        onClick={() => handleExport('excel')}
        loading={exportExcel.isPending}
        disabled={exportPdf.isPending || exportExcel.isPending}
      >
        Export Excel
      </AppButton>
    </div>
  );
}
