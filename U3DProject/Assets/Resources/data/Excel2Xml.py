# conversion program for 'traditional compact data struct'
#
# written by Wang, Kai
# chinagamedev@gmail.com
# 10 May 2013
#

#!/usr/bin/python
# -*- coding: GBK -*-

import os
import xml.etree.ElementTree as ElemTree
import csv
import xlrd
import struct
import traceback  
import sys
import tkinter
import tkinter.ttk
import tkinter.messagebox
from xml.dom import minidom

# 是否打印详细信息，用于调试
DETAIL_INFO = False


DEF_SIZE_DICT = {'string':16, 'int':4, 'float':4}


def detail_info(msg):
    if DETAIL_INFO:
        print(msg)


def msg_box(msg):
    tkinter.messagebox.showinfo('信息', msg)


def err_box(msg):
    tkinter.messagebox.showerror('错误', msg)

    
def trace_back():
    try:
        return traceback.format_exc()  
    except:
        return '' 


def write_float(file, value, size,member,record):
    s=str(float(value))
    record.setAttribute(member,s)
    #print(member,s)
        
def write_int(file, value, size,member,record):    
    s=str(int(value))
    record.setAttribute(member,s)
    #print(member,s)
def write_string(file, value, size,member,record):
    s=str(str(value))
    record.setAttribute(member,s)
    #print(member,s)
def parse_config(path):
    detail_info('正在解析配置文件\'{0}\''.format(path))
	
    root = ElemTree.parse(path)
    #ET.dump(root)
	
    table_name = ''
    table_excel = ''
    table_fields = []
    
    for table in root.iter('table'):
        table_name = table.attrib['name']
        table_excel = table.attrib['ExcelFile']
        
        for fields in table:
            for field in fields:
                #print('\tname:', field.attrib['name'], 'type:', field.attrib['type'])
                name = field.attrib['name']
                type = field.attrib['type']
                size = DEF_SIZE_DICT[type]
                member=field.attrib['member']
				
                if field.attrib.__contains__('size'):
                    size = field.attrib['size']
                    if size == '':
                        size = DEF_SIZE_DICT[type]
                        
                field_info = {'name':name, 'type':type, 'size':size,'member':member}
                table_fields.append(field_info)
            break
        break

    return table_name, table_excel, table_fields
	
def generate(table_fields, xls_path, gen_path):
    detail_info('正在从\'{0}\'生成\'{1}\''.format(xls_path, gen_path))
	
    book = xlrd.open_workbook(xls_path)
    sh = book.sheet_by_index(0)
    row_captions = sh.row_values(0, 0)
    
    for f in table_fields:
        col_index = row_captions.index(f['name'])
        f['colume'] = col_index

    print('gen_path',gen_path)
    fi=open(gen_path,'w+',-1,'utf-8')
    if fi:
        row_count = 0
		
        doc=minidom.Document()
        rootNode=doc.createElement("root")
        doc.appendChild(rootNode)
		
        for rx in range(1, sh.nrows):
            row = sh.row_values(rx)
            if row[0] != '':
                row_count += 1
                
      

        for rx in range(1, sh.nrows):
            row = sh.row_values(rx)
            if row[0] == '':
                continue
            record=doc.createElement("record")
            rootNode.appendChild(record)
			
            for f in table_fields:
                col = f['colume']
                type = f['type']
                size = f['size']
                member=f['member']
                value = row[col]
				
                detail_info('正在导出第{0}行，第{1}列，单元格为\'{2}\''.format(rx + 1, col + 1, xlrd.cellname(rx, col)))
                CALLBACK_DICT[type](fi, value, int(size),member,record)
                
    
    doc.writexml(fi,'\n','\t','\n','utf-8')
    fi.close()			
    return row_count

CALLBACK_DICT = {'string':write_string, 'int':write_int, 'float':write_float}
        
def main():
    config_path = ''
    gen_path = ''
    xls_path = ''

    argc = len(sys.argv)
    if argc != 2 and argc != 3:
        raise Exception('''错误的参数。正确的用法为：ThisProgram.py 配置文件路径 生成文件路径（可不填）''')
    
    if argc == 3:
        config_path = sys.argv[1]
        gen_path = sys.argv[2]
    
    config_path = sys.argv[1]

    table_name, table_excel, table_fields = parse_config(config_path)
    dir_name = os.path.dirname(config_path)
    xls_path = os.path.join(dir_name, table_excel)
    
    if not len(gen_path):
        gen_path = os.path.join(dir_name, table_name + '.xml')
    
    row_count = generate(table_fields, xls_path, gen_path)

    msg = '完成转换！共保存{0}行'.format(row_count)
    print(msg)

    msg_box(msg)

# 执行！
try:
    root = tkinter.Tk()
    root.withdraw()
    main()
except:
    err = trace_back()
    print(err)
    err_box(err)
